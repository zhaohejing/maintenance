using Abp.Dependency;
using Abp.EntityFramework;
using MyCompanyName.AbpZeroTemplate.EntityFramework.Repositories;
using MyCompanyName.AbpZeroTemplate.EntityModel;
using MyCompanyName.AbpZeroTemplate.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.EntityFramework {
    public class SqlExecuter : AbpZeroTemplateRepositoryBase<DeviceFaultInfo>, ISqlExecuter, ITransientDependency {

        public string SqlConn = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        public SqlExecuter(IDbContextProvider<ZhjDbContext> dbContextProvider) : base(dbContextProvider) {

        }




        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>
        /// <returns>执行命令后由数据库返回的结果</returns>
        public int Execute(string sql, params object[] parameters) {
            return MySqlHelper.ExecteNonQuery(SqlConn, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。
        /// </summary>
        /// <typeparam name="T">查询所返回对象的类型</typeparam>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
        /// <returns></returns>
        public IQueryable<T> SqlQuery<T>(string sql, params object[] parameters) {
            return Context.Database.SqlQuery<T>(sql, parameters).AsQueryable<T>();
        }

        public TempModel GetUserClientInfo(string ip) {
            var sql = $@"select top 1 b.strDevName [DeviceName] ,
b.strAssetID [DeviceAssetNo],d.strDevType [DeviceType],e.strUserName [DeviceHead],
b.iDevTypeUsedByTopo [DeviceAlias],b.strDevIdentiy [DeviceSIgnkey],c.strVendor [DeviceManufacture],
b.strLocation [DeviceLocation],b.strDevDesc [DeviceDescription],b.dtDevUpTime  [RepairTime]  from Tbl_ARP a inner join 	Tbl_DevBaseInfo  b on a.strIP=b.strDevIP  and a.strMAC=b.strMAC
left join 	Tbl_DevOID c on b.strDevOID=c.strDevOID left join Tbl_DevType d on b.iDevType=d.iDevType
left join 	Tbl_AlarmConnector e on b.uidUserID = e.uidUserID
where a.strIP='{ip}' and a.iIsValid =1";
            var dt = MySqlHelper.ExecuteDataTable(CommandType.Text, sql);
            return ConvertToModel<TempModel>(dt).FirstOrDefault();
        }

        public List<EnginerInfo> GetEngierList() {
            var sql = $@"select a.UserId,b.UserName,b.Name,b.EmailAddress,e.FullSolve,c.Solving,d.HadSolve from (select UserId from AbpUserRoles where RoleId in (
select Id from AbpRoles where  Name ='工程师')) a left join AbpUsers  b on a.UserId=b.Id
left join (select EnginerId ,count(1) as Solving from DeviceFaultInfo where FaultType=2 group by EnginerId ) c on a.UserId=c.EnginerId
left join (select EnginerId ,count(1) as HadSolve from DeviceFaultInfo where FaultType=3 group by EnginerId ) d on a.UserId=d.EnginerId
left join (select EnginerId ,count(1) as FullSolve from DeviceFaultInfo  group by EnginerId) e  on a.UserId=e.EnginerId";
            var dt = MySqlHelper.ExecuteDataTable(CommandType.Text, sql);
            return ConvertToModel<EnginerInfo>(dt);
        }


        /// <summary>
        /// 将一个SqlDataReader对象转换成一个实体类对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="reader">当前指向的reader</param>
        /// <returns>实体对象</returns>
        public static TEntity MapEntity<TEntity>(SqlDataReader reader) where TEntity : class, new() {
            try {
                var props = typeof(TEntity).GetProperties();
                var entity = new TEntity();
                foreach (var prop in props) {
                    if (prop.CanWrite) {
                        try {

                            var index = reader.GetOrdinal(prop.Name);
                            var data = reader.GetValue(index);
                            if (data != DBNull.Value) {
                                prop.SetValue(entity, Convert.ChangeType(data, prop.PropertyType), null);
                            }
                        }
                        catch (IndexOutOfRangeException) {
                            continue;
                        }
                    }
                }
                return entity;
            }
            catch {
                return null;
            }
        }
        public static List<T> ConvertToModel<T>(DataTable dt) where T : class, new() {
            // 定义集合    
            List<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows) {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys) {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName)) {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
  
}
