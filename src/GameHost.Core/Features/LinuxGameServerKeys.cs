namespace GameHost.Core.Features;

public static class LinuxGameServerKeys
{
    public const string MODULE_NAME = "linuxgameserver";
    public const string SERVER_CONTROL_FOLDER = "server_control";
    public const string SERVER_CONTROL_ASSET_FOLDER = "assets";
    public const string SERVER_CONTROL_CONFIG_FOLDER = "config";
    public const string USERNAME = "lgsm";
    public const string SERVER_INSTALL_PROGRESS_FILE = "installation_progress_state.json";
    public const string SERVER_INSTALL_STATE_FILE = "installation_state.json";
    public const string SERVER_MANIFEST_FILE = "installer_manifest.json";
    public const string SERVER_MANIFEST_RESPO_FILE = "manifest_games.json";

    public static class Queries
    {
        public const string IS_GAME_SERVER_INSTALLED = $"{GameHostKeys.QUERY_PREFIX_V1}.{nameof(LinuxGameServerKeys)}.{nameof(Queries)}.{nameof(IS_GAME_SERVER_INSTALLED)}";
        /// <summary>
        /// Return the Game Id which correspond to the console under server_contro/GAME_ID binary.<br/>
        /// Warning: this can only return when the game server installer is initialized.
        /// </summary>
        public const string GET_GAME_ID = $"{GameHostKeys.QUERY_PREFIX_V1}.{nameof(LinuxGameServerKeys)}.{nameof(Queries)}.{nameof(GET_GAME_ID)}";
        public const string GET_SERVER_INSTALL_STATE = $"{GameHostKeys.QUERY_PREFIX_V1}.{nameof(LinuxGameServerKeys)}.{nameof(Queries)}.{nameof(GET_SERVER_INSTALL_STATE)}";
    }
    public static class Engine { }
    public static class Events
    {
        /// <summary>
        /// Occurs when Game Server Install State Changes, called regardless of the origin oof the installation
        /// </summary>
        public const string ON_GAME_SERVER_INSTALL_STATE_CHANGED = $"{GameHostKeys.EVENT_PREFIX_V1}.{nameof(LinuxGameServerKeys)}.{nameof(Events)}.{nameof(ON_GAME_SERVER_INSTALL_STATE_CHANGED)}";

        /// <summary>
        /// Raised when a game installation is initiated from the dashboard.
        /// </summary>
        public const string ON_GAME_SERVER_INSTALL = $"{GameHostKeys.EVENT_PREFIX_V1}.{nameof(LinuxGameServerKeys)}.{nameof(Events)}.{nameof(ON_GAME_SERVER_INSTALL)}";

        /// <summary>
        /// Event name raised when a game server installation completes.
        /// <para>
        /// This event is emitted only while the dashboard is actively running.  
        /// It may not fire if the game server was installed in the background.  
        /// Do not rely on this event as a definitive installation check; 
        /// </para>
        /// <para>
        /// instead,
        /// use <see cref="LinuxGameServerKeys.Queries.IS_GAME_SERVER_INSTALLED"/>.
        /// </para>
        /// </summary>
        public const string ON_GAME_SERVER_INSTALLED = $"{GameHostKeys.EVENT_PREFIX_V1}.{nameof(LinuxGameServerKeys)}.{nameof(Events)}.{nameof(ON_GAME_SERVER_INSTALLED)}";

        /// <summary>
        /// Event name raised when a game server installation fails to complete.
        /// <para>
        /// This event is emitted only while the dashboard is actively running.  
        /// It may not fire if the game server installation started in the background.  
        /// Do not rely on this event as a definitive installation check; 
        /// </para>
        /// <para>
        /// instead,
        /// use <see cref="LinuxGameServerKeys.Queries.IS_GAME_SERVER_INSTALLED"/>.
        /// </para>
        /// </summary>
        public const string ON_GAME_SERVER_INSTALL_FAILED = $"{GameHostKeys.EVENT_PREFIX_V1}.{nameof(LinuxGameServerKeys)}.{nameof(Events)}.{nameof(ON_GAME_SERVER_INSTALL_FAILED)}";

    }
}
