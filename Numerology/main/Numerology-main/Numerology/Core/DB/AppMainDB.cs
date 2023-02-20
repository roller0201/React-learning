using Core.UnitOfWork;

namespace Core.DB
{
    public  interface IAppMainDB : IUnitOfWork
    {
    }

    public class AppMainDB : UnitOfWork.UnitOfWork, IAppMainDB
    {
        public AppMainDB(string configPath, bool updateSchema) : base(configPath, updateSchema)
        {
        }
    }
}
