﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public interface IUiDispatcher
    {
        void RunSync(Action action);
        void RunAsync(Action action);
        bool CheckAccess();
    }
}
