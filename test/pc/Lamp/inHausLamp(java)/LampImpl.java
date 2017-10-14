/*
 * Project Title : inHausOSGiGateway
 * Author        : Lohrasb jalali
 * Company       : Fraunhofer IMS
 */

package de.inhaus.inHausLamp;
import java.io.IOException;
import de.inhaus.inHausMain.*;
import de.inhaus.inHausBaseDriver.*;
//import org.osgi.framework.*;

/**
 *
 *
 * @author Lohrasb jalali
 * @version
 * @since
 */

public class LampImpl extends DeviceAdmin implements Lamp {

  public InHausBaseDriver inHausBaseDriver=null;
  
  public void setDriver(InHausBaseDriver inHausBaseDriver) {
        o("->setDriver inHausBaeDriver2 = " +inHausBaseDriver);

    this.inHausBaseDriver=inHausBaseDriver;
  }
  
  
  public int getOperatingState() { return 0; }
  public boolean isOn() throws IOException { return false;}
  public void noDriverFound(){}

  private LampActivator parent = null;

  public LampImpl(LampActivator activator) {
    super();
    OID="Lamp";
    parent = activator;
//    senarioMap.put("scenarioChanged","Gesamt");
    FunctionToSuffixMap.put("setIntensity", "Wert");
    FunctionToSuffixMap.put("getIntensity", "Wert");
    FunctionToSuffixMap.put("switchOn", "Status");
    FunctionToSuffixMap.put("switchOff", "Status");
    FunctionToSuffixMap.put("getState", "Status");
    typeToFuncMap.put("Wert","intensityChanged");
    typeToFuncMap.put("Status","stateChanged" );
  }


  public int getIntensity(String deviceID) throws IOException, UnsupportedDeviceOperation {
 
    if (deviceListVector.contains(deviceID)) {
      setSendParameters("getIntensity", deviceID, null);
      if (inHausBaseDriver != null) inHausBaseDriver.pushInhausEventSring(USER_MSG,sendEventToInHaus(false));
      return getDeviceIntValue(deviceID);
    } else return -1;
  }

  public String getIntensityStr(String deviceID) throws IOException, UnsupportedDeviceOperation {
    String retStr="";
    int ret=getIntensity(deviceID);
    retStr=""+ret;
    return retStr;
  }


  public void switchOn(String deviceID) throws UnsupportedDeviceOperation, IOException, IllegalDeviceParameter{
     
    if (deviceListVector.contains(deviceID)) {
    sendEvent("switchOn", deviceID, new Short((short)1), short.class);
//    valueMap.put(deviceID,new String("1"));
    setSendParameters("switchOn", deviceID, "1");
    if (inHausBaseDriver != null) inHausBaseDriver.pushInhausEventSring(USER_MSG,sendEventToInHaus(true));
    }
  }
	
	public void switchOff(String deviceID) throws UnsupportedDeviceOperation, IOException, IllegalDeviceParameter
	{
    if (deviceListVector.contains(deviceID)) {
    sendEvent("switchOff", deviceID, new Short((short)0), short.class);

//    valueMap.put(deviceID,new String("0"));
    funcName="switchOff";
    valueStr="0";
    this.deviceID=deviceID;
    setSendParameters("switchOff", deviceID, "0");
    if (inHausBaseDriver != null) inHausBaseDriver.pushInhausEventSring(USER_MSG,sendEventToInHaus(true));
    }
	}
 
  public void setIntensity(String deviceID,int level)
        throws UnsupportedDeviceOperation, IOException, IllegalDeviceParameter {
    sendEvent("setIntensity", deviceID, new Short((short)level), short.class);

    o("setIntensity");
    if (deviceListVector.contains(deviceID)) {
    
    Integer i=new Integer(level);
    valueMap.put(deviceID,i);
    setSendParameters("setIntensity", deviceID, valueStr=i.toString());
    if (inHausBaseDriver != null) inHausBaseDriver.pushInhausEventSring(USER_MSG,sendEventToInHaus(true));
  
    }
  }
  
  public boolean getState(String deviceID)
            throws UnsupportedDeviceOperation, IOException, IllegalDeviceParameter {
    o("->getState      deviceID="+deviceID);
    int i = 0;
    if (deviceListVector.contains(deviceID)) {
      i = getDeviceIntValue(deviceID);
      setSendParameters("getState", deviceID, null);
      if (inHausBaseDriver != null) inHausBaseDriver.pushInhausEventSring(USER_MSG,sendEventToInHaus(false));
    }
    if (i > 0) return true;
    else return false;
  }
}
