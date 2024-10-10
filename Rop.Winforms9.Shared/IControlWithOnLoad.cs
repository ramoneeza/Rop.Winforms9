using System;
using System.Collections.Generic;
using System.Text;

namespace Rop.Winforms9.Shared;
internal interface IControlWithOnLoad
{
    event ControlEventHandler? FormLoad;
    bool FormCreated { get; }
    bool FormLoaded { get; }
    void OnFormLoad(Form formcontrol);
    void OnLayout(LayoutEventArgs levent);
}
