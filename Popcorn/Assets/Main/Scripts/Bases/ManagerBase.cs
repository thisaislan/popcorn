namespace Popcorn.Bases
{

    public abstract class ManagerBase<T> where T : ManagerBase<T>, new()
    {

        private static T ManageInstance;

        public static T Instance
        {
            get
            {
                if (ManageInstance == null)
                {
                    ManageInstance = new T();
                }
                return ManageInstance;
            }
        }

    }
}
