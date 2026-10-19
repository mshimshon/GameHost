namespace GameHost.Core.Features;

public static class LifecycleKeys
{
    public const string MODULE_NAME = "lifecycle";
    public const string USER_STARTUP_PARAM_FILE = "user_defined_startup_params.json";
    public const string HOST_STARTUP_PARAM_FILE = "host_defined_startup_params.json";
    public const string GAME_INFO_FILE = "game_info.json";
    public const string USER_STARTUP_PARAM_LOCKFILE = $".{USER_STARTUP_PARAM_FILE}.lock";
    public static class Queries
    {
        /// <summary>
        /// Query key for retrieving raw game information.
        /// </summary>
        /// <remarks>
        /// <para><b>Input:</b></para>
        /// <list type="bullet">
        ///   <item>No Input Required</item>
        /// </list>
        /// <para><b>Reply:</b></para>
        /// <list type="bullet">
        ///   <item>
        ///     Type: string
        ///   </item>
        /// </list>
        /// </remarks>
        public const string GET_RAW_GAME_INFO = $"{GameHostKeys.QUERY_PREFIX_V1}.{nameof(LifecycleKeys)}.{nameof(Events)}.{nameof(Queries)}.{nameof(GET_RAW_GAME_INFO)}";

    }
    public static class Engine { }

    /* TODO: Allow Regular Event to use Scheduled Event Keys
     * Schedule Events are One Id to One Handler because they return reschedule information and cannot have multiple scheduled handlers for it.
     * Schedule Event -> Run Period Basis -> ScheduledEventBus Runs -> Also Run Regular Publish on Event using the Scheduled Key.
     */
    public static class Events
    {
        /// <summary>
        /// This is trigger upon Lifecycle GameInfo changed send gameinfo raw data along with the event.
        /// </summary>
        public const string GAMEINFO_STATE_CHANGED = $"{GameHostKeys.EVENT_PREFIX_V1}.{nameof(LifecycleKeys)}.{nameof(Events)}.{nameof(GAMEINFO_STATE_CHANGED)}";

        public static class ServerControl
        {
            /// <summary>
            /// This occur when the stop/start/restart of the server take too long and the transition resets to IDLE in order unlock the UI.
            /// </summary>
            public const string TRANSITION_TIMEDOUT = $"{GameHostKeys.EVENT_PREFIX_V1}.{nameof(LifecycleKeys)}.{nameof(Events)}.{nameof(ServerControl)}.{nameof(TRANSITION_TIMEDOUT)}";
            /// <summary>
            /// This occurs when stop/start/restart has complete and transited to expect state stopped -> started, started -> stopped.
            /// </summary>
            public const string TRANSITION_COMPLETED = $"{GameHostKeys.EVENT_PREFIX_V1}.{nameof(LifecycleKeys)}.{nameof(Events)}.{nameof(ServerControl)}.{nameof(TRANSITION_COMPLETED)}";
        }
        public static class Scheduled
        {
            /// <summary>
            /// Triggers refresh of the server status. (Scheduled Basis)
            /// </summary>
            /// <remarks>
            /// <para><b>Input:</b></para>
            /// <list type="bullet">
            ///   <item>No Input Required</item>
            /// </list>
            /// </remarks>
            public const string GAME_SERVER_INFO_CHECK = $"{GameHostKeys.EVENT_PREFIX_V1}.{nameof(LifecycleKeys)}.{nameof(Events)}.{nameof(Scheduled)}.{nameof(GAME_SERVER_INFO_CHECK)}";

        }
    }


}