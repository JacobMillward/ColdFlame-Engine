using System;
using System.Collections.Generic;

namespace ColdFlame
{

    public abstract class GameSystem
    {
        public List<Entity> actionableEntities = new List<Entity>();
        protected List<Type> actionableComponents = new List<Type>();
        public virtual bool isUnique { get; } = false;
        public virtual int priority { get; } = 0;

        public GameSystem()
        {
            EntityManager.EntityEvent += new EntityManager.EntityEventHandler(OnNotify);
            SystemManager.Add(this);
            Console.WriteLine("Added {0} to SystemMananger", this.GetType().FullName);
        }

        protected virtual void OnNotify(Entity e, string value)
        {
            int componentsMatched = 0;
            foreach (Type type in actionableComponents)
            {
                if (e.ContainsComponent(type))
                {
                    componentsMatched++;
                }
            }

            if (componentsMatched == actionableComponents.Count && !actionableEntities.Contains(e))
            {
                actionableEntities.Add(e);
                Console.WriteLine("{0} Added {1} to actionable entities", this.GetType().FullName, e.ToString());
            }
        }

        public abstract void Update();
    }
}
