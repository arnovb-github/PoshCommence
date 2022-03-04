// https://github.com/fireflycons/PSCloudFormation/blob/master/src/Firefly.PSCloudFormation/PSPathResolver.cs
// Thanks to Alistair McKay
using System.Management.Automation;

namespace PoshCommence.Base
    {
        public class PSPathResolver
        {
            /// <summary>
            /// The path intrinsic
            /// </summary>
            private readonly PathIntrinsics pathIntrinsics;

            /// <summary>
            /// Initializes a new instance of the <see cref="PSPathResolver"/> class.
            /// </summary>
            /// <param name="sessionState">State of the session.</param>
            public PSPathResolver(SessionState sessionState)
            {
                this.pathIntrinsics = sessionState.Path;
            }

            /// <summary>
            /// Resolve a PowerShell path to a .NET path
            /// </summary>
            /// <remarks>
            /// <para>
            /// Try to resolve as a path through the file system provider. PS and .NET have different ideas about the current directory.
            /// .NET path will be whatever the current directory was when PowerShell started, and within PowerShell, it is controlled by
            /// file system provider.
            /// </para>
            /// <para>
            /// If the path was entered as a quoted literal string then those quotes are retained on the argument, so we have to remove them here
            /// </para>
            /// </remarks>
            /// <param name="relativePath">The relative path.</param>
            /// <returns>
            /// Absolute path.
            /// </returns>
            public string ResolvePath(string relativePath)
            {
                if (relativePath == null)
                {
                    return null;
                }

                string resolved = null;

                try
                {
                    resolved = this.pathIntrinsics.GetUnresolvedProviderPathFromPSPath(
                        relativePath);
                }
                catch
                {
                    // do nothing
                }

                return resolved ?? relativePath;
            }

            /// <summary>
            /// Sets the location in the file system.
            /// </summary>
            /// <param name="path">The path to set as current.</param>
            public void SetLocation(string path)
            {
                this.pathIntrinsics.SetLocation(path);
            }

            /// <summary>
            /// Gets the current location in the file system
            /// </summary>
            /// <returns>Current file system location</returns>
            public string GetLocation()
            {
                return this.pathIntrinsics.CurrentLocation.Path;
            }
        }
    }