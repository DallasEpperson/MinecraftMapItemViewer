using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MinecraftMapItemViewer
{
    public class BrowseDialog
    {
        #region API Calls
        [DllImport("ole32.dll")]
        private static extern int CoTaskMemFree(IntPtr hMem);

        [DllImport("kernel32.dll")]
        private static extern IntPtr lstrcat(string lpString1, string lpString2);

        [DllImport("shell32.dll")]
        private static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);

        [DllImport("shell32.dll")]
        private static extern int SHGetPathFromIDList(IntPtr pidList, StringBuilder lpBuffer);
        #endregion

        public struct BROWSEINFO
        {
            public IntPtr hWndOwner;
            public int pIDLRoot;
            public string pszDisplayName;
            public string lpszTitle;
            public int ulFlags;
            public int lpfnCallback;
            public int lParam;
            public int iImage;
        }

        const int MAX_PATH = 260;

        [Flags]
        public enum BrowseForOptions
        {
            //ref: http://msdn.microsoft.com/en-us/library/windows/desktop/bb773205%28v=vs.85%29.aspx
            None                            = 0,
            ReturnOnlyFileSystemDirectories = 1 << 0,
            DontGoBelowDomain               = 1 << 1,
            StatusText                      = 1 << 2,
            ReturnFileSystemAncestors       = 1 << 3,
            EditBox                         = 1 << 4, 
            Validate                        = 1 << 5,
            NewDialogStyle                  = 1 << 6,
            BrowseIncludeURLs               = 1 << 7,
            UsageHint                       = 1 << 8,
            NoNewFolderButton               = 1 << 9,
            NoTranslateTargets              = 1 << 10,
            // 1 << 11 not used
            BrowseForComputer               = 1 << 12,
            BrowseForPrinter                = 1 << 13,
            BrowseIncludeFiles              = 1 << 14,
            Shareable                       = 1 << 15,
            BrowseFileJunctions             = 1 << 16,
            UseNewUI                        = EditBox | NewDialogStyle

        }

        private BrowseForOptions m_BrowseFor = BrowseForOptions.ReturnOnlyFileSystemDirectories;
        private string m_Title = "";
        private string m_Selected = "";

        

        protected Boolean RunDialog(IntPtr hWndOwner)
        {
            BROWSEINFO udtBI = new BROWSEINFO();
            IntPtr lpIDList;
            GCHandle hTitle = GCHandle.Alloc(Title, GCHandleType.Pinned);
            udtBI.hWndOwner = hWndOwner;
            udtBI.lpszTitle = Title;
            udtBI.ulFlags = (int)BrowseFor;
            StringBuilder buffer = new StringBuilder(MAX_PATH);
            buffer.Length = MAX_PATH;
            udtBI.pszDisplayName = buffer.ToString();
            lpIDList = SHBrowseForFolder(ref udtBI);
            hTitle.Free();
            if (lpIDList.ToInt64() != 0)
            {
                if (BrowseFor == BrowseForOptions.BrowseForComputer)
                {
                    m_Selected = udtBI.pszDisplayName.Trim();
                }
                else
                {
                    StringBuilder path = new StringBuilder(MAX_PATH);
                    SHGetPathFromIDList(lpIDList, path);
                    m_Selected = path.ToString();
                }
                CoTaskMemFree(lpIDList);
            }
            else
            {
                return false;
            }
            return true;
        }

        public DialogResult ShowDialog()
        {
            return ShowDialog(null);
        }

        public DialogResult ShowDialog(IWin32Window owner)
        {
            IntPtr handle;
            if (owner != null)
            {
                handle = owner.Handle;
            }
            else
            {
                handle = IntPtr.Zero;
            }

            if (RunDialog(handle))
            {
                return DialogResult.OK;
            }
            else
            {
                return DialogResult.Cancel;
            }
        }

        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                m_Title = value;
            }
        }

        public string Selected
        {
            get
            {
                return m_Selected;
            }
        }

        public BrowseForOptions BrowseFor
        {
            get
            {
                return m_BrowseFor;
            }
            set
            {
                m_BrowseFor = value;
            }
        }

        

        public void New()
        {
        }

    }
}
