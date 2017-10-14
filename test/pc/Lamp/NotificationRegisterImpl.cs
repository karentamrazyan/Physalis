/*
* Project Title : inHausOSGiGateway
* Author        : Lohrasb jalali
* Company       : Fraunhofer IMS
*/
namespace inHausLamp
{
	using System;
	using NotificationRegister = de.berkom.ihome.coreservices.notification.lib.NotificationRegister;
	using NotificationListener = de.berkom.ihome.coreservices.notification.lib.NotificationListener;
	//import de.tnova.training.notificationproxy.lib.NotificationListener;
	//import de.tnova.training.notificationproxy.lib.NotificationRegister;
	
	/// <summary>*
	/// *
	/// </summary>
	/// <author>  Lohrasb jalali
	/// </author>
	/// <version> 
	/// @since
	/// 
	/// </version>
	public class NotificationRegisterImpl : NotificationRegister
	{
		private NotificationListener listener = null;
		/// <summary>*
		/// *
		/// </summary>
		/// <param name="">param0
		/// 
		/// </param>
		public virtual void  addNotificationListener(NotificationListener param0)
		{
			System.Console.Out.WriteLine("Neuer Listener registriert!");
			
			listener = param0;
			//   notifyListener("Hello Telekom");
		}
		
		/// <summary>*
		/// *
		/// </summary>
		/// <param name="">param0
		/// 
		/// </param>
		public virtual void  removeNotificationListener(NotificationListener param0)
		{
			listener = null;
		}
		
		/// <summary>*
		/// *
		/// </summary>
		/// <param name="">msg
		/// 
		/// </param>
		public virtual void  notifyListener(System.String msg)
		{
			if (listener != null)
			{
				listener.push(msg, msg, msg);
			}
		}
	}
}