using Popcorn.Bases;
using Popcorn.Core;
using Popcorn.Metadatas;
using UnityEngine.SceneManagement;
using ScenesNames = Popcorn.Metadatas.Scenes.Names;

namespace Popcorn.Managers
{

    public sealed class ScenesManager : ManagerBase<ScenesManager>
    {

        public void CallNextScene()
        {
            ScenesNames nextSceneName;

            switch (App.CurrentSceneName)
            {
                case ScenesNames.World1Scene1:
                    nextSceneName = ScenesNames.World1Scene1;
                    break;
                default:
                    nextSceneName = ScenesNames.World1Scene1;
                    break;
            }
            App.CurrentSceneName = nextSceneName;
            SceneManager.LoadScene(nextSceneName.ToString());
        }

    }
}