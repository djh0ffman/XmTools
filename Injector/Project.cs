using System.Collections.Generic;
using XMSerializer;

namespace Injector
{
    class Project
    {
        public string ModuleFileName { get; set; }
        public byte[] SourceModule { get; set; }
        public ExtendedModule XM { get; set; }
        public List<InjectionPlan> Plans { get; set; }
        public int Channels { get; set; }
        public bool CentreSourceModule { get; set; }
        public List<InjectionStill> Stills { get; set; }
        public Project()
        {
            Plans = new List<InjectionPlan>();
            Stills = new List<InjectionStill>();
        }
    }
}
