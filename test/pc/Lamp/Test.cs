//package inHausLamp;
namespace de.inhaus.inHausLamp
{
	using System;
	using UnsupportedDeviceOperation = de.inhaus.inHausMain.UnsupportedDeviceOperation;
	using IllegalDeviceParameter = de.inhaus.inHausMain.IllegalDeviceParameter;
	using Device = de.inhaus.inHausMain.Device;
	using org.osgi.framework;
	
	public class Test
	{
		
		private ServiceReference[] services = new ServiceReference[]{};
		private BundleContext context = null;
		
		public Test(BundleContext con)
		{
			context = con;
		}
		
		public virtual void  start()
		{
			try
			{
				System.String filter = "(&(THRC.Identifier=Lamp)(THRC.Interface=de.inhaus.inHausLamp.Lamp))";
				//      String filter = "(name=LampDevice)";
				System.Object obj = null;
				services = context.getServiceReferences("de.inhaus.inHausLamp.Lamp", filter);
				//      services = context.getServiceReferences("org.osgi.service.device.Device", filter);
				if (services != null)
					obj = context.getService(services[0]);
				if (obj != null)
				{
					System.Type c = obj.GetType();
					System.Reflection.MethodInfo[] methods = c.GetMethods();
					//  System.out.println("search methodes");
					for (int i = 0; i < methods.Length; i++)
					{
						//  System.out.println("methode no"+i+" = "+ methods[i].getName());
						if (methods[i].Name.Equals("getIntensity"))
						{
							System.Object[] param0 = new System.Object[]{new System.String("LH_WZ_LAMPE_100".ToCharArray())};
							methods[i].Invoke(obj, (System.Object[]) param0);
						}
						else if (methods[i].Name.Equals("setIntensity"))
						{
							System.Object[] param = new System.Object[]{new System.String("LH_WZ_LAMPE_100".ToCharArray()), (int) 20};
							methods[i].Invoke(obj, (System.Object[]) param);
						}
					}
				}
				else
					System.Console.Out.WriteLine("obj == null");
			}
			catch (System.Exception ex)
			{
				SupportClass.WriteStackTrace(ex, Console.Error);
			}
		}
	}
}