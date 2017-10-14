//package inHausLamp;
package de.inhaus.inHausLamp;

import java.io.IOException;
import de.inhaus.inHausMain.UnsupportedDeviceOperation;
import de.inhaus.inHausMain.IllegalDeviceParameter;
import de.inhaus.inHausMain.Device;

import java.lang.reflect.*;
import org.osgi.framework.*;

public class Test {
 
  private ServiceReference[] services = {};
  private BundleContext context = null;

  public Test(BundleContext con) {
    context = con;
  }

  public void start() {
    try {
      String filter = "(&(THRC.Identifier=Lamp)(THRC.Interface=de.inhaus.inHausLamp.Lamp))";
//      String filter = "(name=LampDevice)";
      Object obj = null;
      services = context.getServiceReferences("de.inhaus.inHausLamp.Lamp", filter);
//      services = context.getServiceReferences("org.osgi.service.device.Device", filter);
      if (services != null)
        obj = context.getService(services[0]);
      if (obj != null) {
        Class c = obj.getClass();
        Method[] methods = c.getMethods();
      //  System.out.println("search methodes");
        for (int i=0;i<methods.length;i++) {
        //  System.out.println("methode no"+i+" = "+ methods[i].getName());
          if (methods[i].getName().equals( "getIntensity")){
            Object[] param0 = {new String("LH_WZ_LAMPE_100")};
            methods[i].invoke(obj,param0);
          }
          else if (methods[i].getName().equals("setIntensity")) {
            Object[] param = {new String("LH_WZ_LAMPE_100"),new Integer((int)20)};
            methods[i].invoke(obj,param);
          }
        }
      } else System.out.println("obj == null");
    } catch (Exception ex) {ex.printStackTrace();}
  }
}




