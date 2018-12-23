namespace Popcorn.Metadatas
{
    public static class Strings
    {

        public class Errors
        {
            public const string EndpointNotFound = "Endpoint not found in screen. Please put the Endpoint in scene to continue";
            public const string MultipleEndpointsFound = "Multiple Endpoints found in screen.  Please remove multiple instances of Endpoint to continue";
            public const string StartPointNotFound = "Start point not found in screen. Please put the start point in scene to continue";
            public const string MultipleStartPointsFound = "Multiple start point found in screen.  Please remove multiple instances of start point to continue";
            public const string PlayerNotFound = "Player not found in screen. Please put the player in scene to continue";
            public const string MultiplePlayersFound = "Multiple players found in screen. Please remove multiple instances of player to continue";
            public const string AnyLimitViewNotFound = "Any LimitView not found in screen. Please put all LimitViews in scene to continue";
            public const string AnyLimitViewWithTheWrongTag = "Any LimitView object with the wrong tag. Please put correct tag to continue";
            public const string AnyLimitViewWithTheWrongLimitSide = "Any LimitView object with the wrong LimitSide. Please put correct LimitSide to continue";
            public const string AnyLimitViewIsMultiplied = "Any LimitView object is multiplied. Please fix it to continue";
            public const string CruzedVerticalLimits = "The LimitView vertical objects are cruzed. Please fix it to continue";
            public const string CruzedHorizontalLimits = "The LimitView horizontal objects are cruzed. Please fix it to continue";
            public const string StartAndEndpointCruzed = "The start point and and point are cruzed. Please fix it to continue";
            public const string DefaultErrorObjectNotFound = "Object not found in scene";
            public const string defaultErrorComponentNotFound = "Component not found";
            public const string DefaultErrorComponentInChildNotFound = "Component in child not found, don't existe components in child";
            public const string DefaultErrorComponentInChildNotFoundInPosition = "Component in child not found in the indicated position";
            public const string MultipleDestructorsFound = "Multiple destructor found in screen.  Please remove multiple instances of destructor to continue";
            public const string MultipleTimesFound = "Multiple time found in screen.  Please remove multiple instances of yime to continue";
            public const string TimeNotFound = "Time not found in screen. Please put the time in scene to continue";
            public const string TimeNotSetInGameBehavior = "Time not set in game behaior. Please put the time in game behavior to continue";
            public const string CameraNotFound = "Camera not found in screen. Please put the camera in scene to continue";
            public const string MultipleCamerasFound = "Multiple camera found in screen.  Please remove multiple instances of camera to continue";
            public const string TriggerNotFoundInRunWhenPlayerIsNear = "Trigger not found in object runby trigger type";
            public const string WrongPlayerTag = "The player object has wrong tag";
            public const string WrongEnemyTag = "Any enemy object has wrong tag";
            public const string TriggerNotFoundInATriggerHelper = "Trigger not found in object trigger helper type";
            public const string AttemptStopDontInitializedBackgroundMusic = "Attempt to stop background music that was not started";
        }

        public class ErrorsAuxs
        {
            public const string Caller = "Caller: ";
            public const string Tag = "Tag: ";
            public const string GameObject = "GameObject: ";            
            public const string Type = "Type: ";
            public const string Position = "Position: ";
        }

        public class CombineCharacters
        {
            public const string SpaceColonSpace = " : ";
            public const string CommaSpace = ", ";
        }

    }
    
}