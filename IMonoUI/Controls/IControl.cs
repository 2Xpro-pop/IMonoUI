using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMonoUI.Controls;
public interface IControl
{
    public void Render(IRendererContext context);
}
