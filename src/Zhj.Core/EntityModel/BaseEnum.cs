using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.EntityModel {
    public enum AttachType {
        Image = 1, File = 2
    }
    public enum KnowledgeType {
        ComputerKnowledge = 1, FaultKnowledge = 2
    }
    public enum KnowledgeState {
        NotSolved = 1, HadSolved = 2
    }
    public enum FaultType {
        NotSolved = 1,
        Solving = 2,
        HadSolved = 3
    }
}
