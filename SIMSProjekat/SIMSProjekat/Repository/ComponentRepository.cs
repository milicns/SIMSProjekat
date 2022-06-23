using SIMSProjekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Repository
{
    public class ComponentRepository
    {
        private Serializer<Component> componentSerializer = new Serializer<Component>();
        private List<Component> components;
        private string filePath = @"..\..\..\SIMSProjekat\Data\components.json";


        public List<Component> GetAllComponents()
        {
            components = componentSerializer.JsonDeserialize(filePath) as List<Component>;
            return components;
        }

        public void SaveComponents(List<Component> components)
        {

            componentSerializer.JsonSerialize(components, filePath);

        }

        public void AddComponent(Component component)
        {
            components = GetAllComponents();
            components.Add(component);
            SaveComponents(components);
        }

        public void DeleteComponent(Component component)
        {
            components = GetAllComponents();
            components.Remove(component);
            SaveComponents(components);
        }

        public Component FindByKey(String key)
        {
            components = GetAllComponents();
            foreach (Component component in components)
            {
                if (component.Name.Equals(key))
                {
                    return component;
                }
            }
            return null;
        }
    }
}
