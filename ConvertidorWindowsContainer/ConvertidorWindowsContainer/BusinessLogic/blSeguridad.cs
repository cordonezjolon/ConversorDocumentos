using System.Security.Principal;

namespace MS_ConvertidorDocumentos.BusinessLogic
{
    public class blSeguridad
    {
        private String usuario { get; set; }
        private String dominio { get; set; }
        private String password { get; set; }
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern long RevertToSelf();
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern long CloseHandle(IntPtr handle);

        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern int LogonUserA(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern int DuplicateToken(IntPtr ExistingTokenHandle, int ImpersonationLevel, ref IntPtr DuplicateTokenHandle);

        int LOGON32_LOGON_NEW_CREDENTIALS = 9;
        int LOGON32_PROVIDER_DEFAULT = 0;

       // WindowsImpersonationContext impersonationContext;

        private void undoImpersonation()
        {
            //impersonationContext.Undo();
        }

        public bool impersonateValidUser()
        {
            //WindowsIdentity tempWindowsIdentity;
            //IntPtr token = IntPtr.Zero;
            //IntPtr tokenDuplicate = IntPtr.Zero;
            //bool RimpersonateValidUser = false;
            //if (RevertToSelf() == 1)
            //{
            //    if (LogonUserA(this.usuario, this.dominio, this.password, LOGON32_LOGON_NEW_CREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
            //    {
            //        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
            //        {
            //            tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
            //            impersonationContext = tempWindowsIdentity.;
            //            if (impersonationContext != null)
            //                RimpersonateValidUser = true;
            //        }
            //    }
            //}
            //if (!tokenDuplicate.Equals(IntPtr.Zero))
            //    CloseHandle(tokenDuplicate);
            //if (!token.Equals(IntPtr.Zero))
            //    CloseHandle(token);

            return true;
        }

        public blSeguridad(String _usuario, String _dominio, String _password)
        {
            this.usuario = _usuario;
            this.password = _password;
            this.dominio = _dominio;
        }
    }
}
