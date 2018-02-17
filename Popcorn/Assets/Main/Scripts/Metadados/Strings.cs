namespace Popcorn.Metadados
{
    public static class Strings
    {

        public class Errors
        {
            public const string ENDPOINT_NOT_FOUND = "Endpoint not found in screen. Please put the Endpoint in scene to continue";
            public const string MULTIPLE_ENDPOINT_FOUND = "Multiple Endpoints found in screen.  Please remove multiple instances of Endpoint to continue";
            public const string START_POINT_NOT_FOUND = "Start point not found in screen. Please put the start point in scene to continue";
            public const string MULTIPLE_START_POINT_FOUND = "Multiple start point found in screen.  Please remove multiple instances of start point to continue";
            public const string PLAYER_NOT_FOUND = "Player not found in screen. Please put the player in scene to continue";
            public const string MULTIPLE_PLAYERS_FOUND = "Multiple players found in screen. Please remove multiple instances of player to continue";
            public const string ANY_LIMIT_VIEW_NOT_FOUND = "Any LimitView not found in screen. Please put all LimitViews in scene to continue";
            public const string ANY_LIMIT_VIEW_WITH_THE_WRONG_TAG = "Any LimitView object with the wrong tag. Please put correct tag to continue";
            public const string ANY_LIMIT_VIEW_WITH_THE_WRONG_LIMIT_SIDE = "Any LimitView object with the wrong LimitSide. Please put correct LimitSide to continue";
            public const string ANY_LIMIT_VIEW_IS_MULTIPLIED = "Any LimitView object is multiplied. Please fix it to continue";
            public const string CRUZED_VERTICAL_LIMITS = "The LimitView vertical objects are cruzed. Please fix it to continue";
            public const string CRUZED_HORIZONTAL_LIMITS = "The LimitView horizontal objects are cruzed. Please fix it to continue";
            public const string START_AND_ENDPOINT_CRUZED = "The start point and and point are cruzed. Please fix it to continue";
            public const string DEFAULT_ERROR_OBJECT_NOT_FOUND = "Object not found in scene";
            public const string DEFAULT_ERROR_COMPONENT_NOT_FOUND = "Component not found";
            public const string DEFAULT_ERROR_COMPONENT_IN_CHILD_NOT_FOUND = "Component in child not found, don't existe components in child";
            public const string DEFAULT_ERROR_COMPONENT_IN_CHILD_NOT_FOUND_IN_POSITION = "Component in child not found in the indicated position";
            public const string MULTIPLE_DESTRUCTOR_FOUND = "Multiple destructor found in screen.  Please remove multiple instances of destructor to continue";
            public const string MULTIPLE_TIME_FOUND = "Multiple time found in screen.  Please remove multiple instances of yime to continue";
            public const string TIME_NOT_FOUND = "Time not found in screen. Please put the time in scene to continue";
            public const string TIME_NOT_SET_IN_GAME_BEHAVIOR = "Time not set in game behaior. Please put the time in game behavior to continue";
            public const string CAMERA_NOT_FOUND = "Camera not found in screen. Please put the camera in scene to continue";
            public const string MULTIPLE_CAMERA_FOUND = "Multiple camera found in screen.  Please remove multiple instances of camera to continue";
            public const string TRIGGER_NOT_FOUND_IN_RUN_WHEN_PLAYER_IS_NEAR = "Trigger not found in object runby trigger type";
            public const string WRONG_PLAYER_TAG = "The player objecto has wrong tag";
            public const string WRONG_ENEMY_TAG = "Any enemy objecto has wrong tag";
            public const string TRIGGER_NOT_FOUND_IN_A_TRIGGER_HELPER = "Trigger not found in object trigger helper type";
            public const string ATTEMPT_STOP_DONT_INITIALIZED_BACKGROUND_MUSIC = "Attempt to stop background music that was not started";
        }

        public class ErrorsAuxs
        {
            public const string CALLER = "Caller: ";
            public const string TAG = "Tag: ";
            public const string GAME_OBJECT = "GameObject: ";            
            public const string TYPE = "Type: ";
            public const string POSITION = "Position: ";
        }

        public class CombineCharacters
        {
            public const string SPACE_COLON_SPACE = " : ";
            public const string COMMA_SPACE = ", ";
        }

    }
}