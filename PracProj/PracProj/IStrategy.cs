using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracProj {
    public interface IStrategy {
        void Action(Monster monster);
    }
}
