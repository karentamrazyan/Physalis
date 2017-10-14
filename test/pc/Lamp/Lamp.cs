namespace de.inhaus.inHausLamp
{
	using System;
	using Physalis.Framework;
	using Device = Physalis.Framework.Service.Device;
	//using UnsupportedDeviceOperation = de.inhaus.inHausMain.UnsupportedDeviceOperation;
	//using IllegalDeviceParameter = de.inhaus.inHausMain.IllegalDeviceParameter;
	//using Device = de.inhaus.inHausMain.Device;
	//using org.osgi.framework;
	public interface Lamp : Device
		{
			/// <summary> This method will return light intensity in percents.
			/// </summary>
			int getIntensity(System.String deviceID);
			System.String getIntensityStr(System.String deviceID);
			/// <summary> This method will set light intensity in percents.
			/// </summary>
			void  setIntensity(System.String deviceID, int level);
			/// <summary> This method will switch on the light 
			/// </summary>
			void  switchOn(System.String deviceID);
			/// <summary> This method will switch off the light 
			/// </summary>
			void  switchOff(System.String deviceID);
			bool getState(System.String deviceID);
		}
}