namespace Popcorn.Bases
{

    public abstract class ManagerBase<T> where T : ManagerBase<T>, new()
    {

        static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null) { instance = new T(); }
                return instance;
            }
        }

    }

}