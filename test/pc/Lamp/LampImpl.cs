/*
* Project Title : inHausOSGiGateway
* Author        : Lohrasb jalali
* Company       : Fraunhofer IMS
*/
namespace de.inhaus.inHausLamp
{
	using System;
	using de.inhaus.inHausMain;
	using de.inhaus.inHausBaseDriver;
	//import org.osgi.framework.*;
	
	/// <summary>*
	/// *
	/// </summary>
	/// <author>  Lohrasb jalali
	/// </author>
	/// <version> 
	/// @since
	/// 
	/// </version>
	
	public class LampImpl:DeviceAdmin, Lamp
	{
		virtual public InHausBaseDriver Driver
		{
			set
			{
				o("->setDriver inHausBaeDriver2 = " + value);
				
				this.inHausBaseDriver = value;
			}
			
		}
		virtual public int OperatingState
		{
			get
			{
				return 0;
			}
			
		}
		virtual public bool On
		{
			get
			{
				return false;
			}
			
		}
		
		public InHausBaseDriver inHausBaseDriver = null;
		
		
		
		public virtual void  noDriverFound()
		{
		}
		
		private LampActivator parent = null;
		
		public LampImpl(LampActivator activator):base()
		{
			OID = "Lamp";
			parent = activator;
			//    senarioMap.put("scenarioChanged","Gesamt");
			FunctionToSuffixMap.put("setIntensity", "Wert");
			FunctionToSuffixMap.put("getIntensity", "Wert");
			FunctionToSuffixMap.put("switchOn", "Status");
			FunctionToSuffixMap.put("switchOff", "Status");
			FunctionToSuffixMap.put("getState", "Status");
			typeToFuncMap.put("Wert", "intensityChanged");
			typeToFuncMap.put("Status", "stateChanged");
		}
		
		
		public virtual int getIntensity(System.String deviceID)
		{
			
			if (deviceListVector.contains(deviceID))
			{
				setSendParameters("getIntensity", deviceID, null);
				if (inHausBaseDriver != null)
					inHausBaseDriver.pushInhausEventSring(USER_MSG, sendEventToInHaus(false));
				return getDeviceIntValue(deviceID);
			}
			else
				return - 1;
		}
		
		public virtual System.String getIntensityStr(System.String deviceID)
		{
			System.String retStr = "";
			int ret = getIntensity(deviceID);
			retStr = "" + ret;
			return retStr;
		}
		
		
		public virtual void  switchOn(System.String deviceID)
		{
			
			if (deviceListVector.contains(deviceID))
			{
				sendEvent("switchOn", deviceID, (short) 1, typeof(short));
				//    valueMap.put(deviceID,new String("1"));
				setSendParameters("switchOn", deviceID, "1");
				if (inHausBaseDriver != null)
					inHausBaseDriver.pushInhausEventSring(USER_MSG, sendEventToInHaus(true));
			}
		}
		
		public virtual void  switchOff(System.String deviceID)
		{
			if (deviceListVector.contains(deviceID))
			{
				sendEvent("switchOff", deviceID, (short) 0, typeof(short));
				
				//    valueMap.put(deviceID,new String("0"));
				funcName = "switchOff";
				valueStr = "0";
				this.deviceID = deviceID;
				setSendParameters("switchOff", deviceID, "0");
				if (inHausBaseDriver != null)
					inHausBaseDriver.pushInhausEventSring(USER_MSG, sendEventToInHaus(true));
			}
		}
		
		public virtual void  setIntensity(System.String deviceID, int level)
		{
			sendEvent("setIntensity", deviceID, (short) level, typeof(short));
			
			o("setIntensity");
			if (deviceListVector.contains(deviceID))
			{
				
				System.Int32 i = level;
				valueMap.put(deviceID, i);
				setSendParameters("setIntensity", deviceID, valueStr = i.ToString());
				if (inHausBaseDriver != null)
					inHausBaseDriver.pushInhausEventSring(USER_MSG, sendEventToInHaus(true));
			}
		}
		
		public virtual bool getState(System.String deviceID)
		{
			o("->getState      deviceID=" + deviceID);
			int i = 0;
			if (deviceListVector.contains(deviceID))
			{
				i = getDeviceIntValue(deviceID);
				setSendParameters("getState", deviceID, null);
				if (inHausBaseDriver != null)
					inHausBaseDriver.pushInhausEventSring(USER_MSG, sendEventToInHaus(false));
			}
			if (i > 0)
				return true;
			else
				return false;
		}
	}
}