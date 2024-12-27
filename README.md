## 07 NOV 2024
> The MemoryMonitor component is a tool for monitoring memory usage in a Unity project. It displays the total memory reserved, used memory, and available memory directly in your UI using a TMP_Text display. The component updates memory information at a specified interval, providing real-time feedback on memory consumption in the scene.

> INSTALLATION:
1. Add the MemoryMonitor prefab to the starting scene of your project.
2. Ensure you have TextMeshPro imported in your project, as it’s required for displaying memory data.

> SETUP:
1. Place the MemoryMonitor prefab in the scene hierarchy.

> IN THE INSPECTOR:
1. Memory Display: Link a TMP_Text component to display memory statistics.
2. Refresh Interval: Set the interval (in seconds) for refreshing the memory display. The default is 1 second.
 
> SCRIPT DETAILS:
The MemoryMonitor script does the following:
1. Awake: Ensures only one instance (_sInstance) of the MemoryMonitor exists in the scene.
2. Start: Initializes the memory display.
3. Update: Checks the time elapsed since the last memory update. If the specified refresh interval has passed, it updates the memory display.
4. UpdateMemoryDisplay: Fetches memory statistics using UnityEngine.Profiling and displays:
5. Total Reserved Memory: Total memory reserved by the application.
6. Used Memory: Total allocated memory.
7. Available Memory: Memory reserved but not in use.

> USAGE:
1. To use MemoryMonitor for tracking addressable asset memory usage, add new addressable assets to _addressableMemoryUsage. Each entry associates the asset with a unique identifier.