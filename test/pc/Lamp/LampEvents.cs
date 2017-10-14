/*
* Project Title : inHausOSGiGateway
* Author        : Lohrasb jalali
* Company       : Fraunhofer IMS
*/
namespace de.inhaus.inHausLamp
{
	using System;
	
	/// <summary> 
	/// 
	/// </summary>
	/// <author>  Lohrasb jalali
	/// </author>
	/// <version>  
	/// @since 
	/// 
	/// </version>
	public interface LampEvents
		{
			void  stateChanged(System.String deviceID, bool state);
			void  intensityChanged(System.String deviceID, int intensity);
		}
}