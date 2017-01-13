using MyCompanyName.AbpZeroTemplate.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.Storage {
    public interface ISqlExecuter {
        TempModel GetUserClientInfo(string ip);
        List<EnginerInfo> GetEngierList();
    }
}
