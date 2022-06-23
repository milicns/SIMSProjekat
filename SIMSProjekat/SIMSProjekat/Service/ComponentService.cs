using SIMSProjekat.Model;
using SIMSProjekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Service
{
    public class ComponentService
    {
        private ComponentRepository componentRepository = new ComponentRepository();

        public List<Component> GetAllComponents()
        {
            return componentRepository.GetAllComponents();
        }

        public void AddComponent(Component component)
        {
            componentRepository.AddComponent(component);
        }

        public void DeleteComponent(Component component)
        {
            componentRepository.DeleteComponent(component);
        }

        public Component FindByName(String name)
        {
            return componentRepository.FindByKey(name);
        }
    }
}
