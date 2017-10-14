package de.inhaus.inHausLamp;

import java.io.IOException;
import de.inhaus.inHausMain.UnsupportedDeviceOperation;
import de.inhaus.inHausMain.IllegalDeviceParameter;
import de.inhaus.inHausMain.Device;
import org.osgi.framework.*;
public interface Lamp extends Device
{
		
	/**
	 * This method will return light intensity in percents.
	 * @exception IOException
	 * @exception UnsupportedDeviceOperation
	 */
	public int getIntensity(String deviceID)
		throws IOException, UnsupportedDeviceOperation;
	public String getIntensityStr(String deviceID)
		throws IOException, UnsupportedDeviceOperation;

	/**
	 * This method will set light intensity in percents.
	 * @param level
	 * @exception UnsupportedDeviceOperation
	 * @exception IOException
	 * @exception IllegalDeviceParameter
	 */
	public void setIntensity(String deviceID,int level)
		throws UnsupportedDeviceOperation, IOException, IllegalDeviceParameter;
  
	/**
	 * This method will switch on the light 
	 * @exception UnsupportedDeviceOperation
	 * @exception IOException
	 * @exception IllegalDeviceParameter
	 */
	public void switchOn(String deviceID)
		throws UnsupportedDeviceOperation, IOException, IllegalDeviceParameter;
  
  
	/**
	 * This method will switch off the light 
	 * @exception UnsupportedDeviceOperation
	 * @exception IOException
	 * @exception IllegalDeviceParameter
	 */
	public void switchOff(String deviceID)
		throws UnsupportedDeviceOperation, IOException, IllegalDeviceParameter;
		
	public boolean getState(String deviceID) 
	  throws UnsupportedDeviceOperation, IOException, IllegalDeviceParameter;
  
  //public String[] getDeviceList(); ist von Deviceadmin geerbt.

}

