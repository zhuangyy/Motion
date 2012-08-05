using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace ZForge.Win32
{
	public class DllRegSvr
	{

		private string mFileName;
		private static ModuleBuilder mModuleBuilder;
		private Type m_tDllReg;


		public DllRegSvr(string dllFile)
		{
			mFileName = dllFile;
			CreateDllRegType();
		}


		public void Register()
		{
			InternalRegServer(false);
		}

		public void UnRegister()
		{
			InternalRegServer(true);
		}

		private void InternalRegServer(bool fUnreg)
		{
			string sMemberName = fUnreg ? "DllUnregisterServer" : "DllRegisterServer";

			int hr = (int)m_tDllReg.InvokeMember(sMemberName, BindingFlags.InvokeMethod, null,
																						Activator.CreateInstance(m_tDllReg), null);
			if (hr != 0)
				Marshal.ThrowExceptionForHR(hr);
		}

		private void CreateDllRegType()
		{
			if (mModuleBuilder == null)
			{
				// Create dynamic assembly    
				AssemblyName an = new AssemblyName();
				an.Name = "DllRegServerAssembly" + Guid.NewGuid().ToString("N");
				AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);

				// Add module to assembly
				mModuleBuilder = ab.DefineDynamicModule("DllRegServerModule");
			}

			// Add class to module
			TypeBuilder tb = mModuleBuilder.DefineType("DllRegServerClass" + Guid.NewGuid().ToString("N"));

			MethodBuilder meb;

			// Add PInvoke methods to class
			meb = tb.DefinePInvokeMethod("DllRegisterServer", mFileName,
				MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl,
				CallingConventions.Standard, typeof(int), null, CallingConvention.StdCall, CharSet.Auto);

			// Apply preservesig metadata attribute so we can handle return HRESULT ourselves
			meb.SetImplementationFlags(MethodImplAttributes.PreserveSig | meb.GetMethodImplementationFlags());

			meb = tb.DefinePInvokeMethod("DllUnregisterServer", mFileName,
				MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl,
				CallingConventions.Standard, typeof(int), null, CallingConvention.StdCall, CharSet.Auto);

			// Apply preservesig metadata attribute so we can handle return HRESULT ourselves
			meb.SetImplementationFlags(MethodImplAttributes.PreserveSig | meb.GetMethodImplementationFlags());

			// Create the type
			m_tDllReg = tb.CreateType();
		}

	}
}
