using System;
using SIMSProjekat.Service;
using SIMSProjekat.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Controller
{
    internal class ComponentController
    {
        private ComponentService componentService = new ComponentService();

        public List<Component> GetAllComponents()
        {
            return componentService.GetAllComponents();
        }

        public void AddComponent(Component component)
        {
            componentService.AddComponent(component);
        }

        public void DeleteComponent(Component component)
        {
            componentService.DeleteComponent(component);
        }

        public Component FindByName(String name)
        {
            return componentService.FindByName(name);
        }
    }
}
