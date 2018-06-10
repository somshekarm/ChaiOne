using System;

namespace ManifestModels
{
    public class AppServer : BaseModel
    {
        public AppServer()
        {
        }

        public AppServer(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
