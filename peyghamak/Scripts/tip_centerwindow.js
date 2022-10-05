/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//tip_centerwindow.js
//Version: 2.1
//This script is created by Alireza Poshtkohi. Do not remove, modify, or hide the author information. keep it intact.
//Mail: alireza.poshtkohi@gmail.com 

// Make sure that the core file wz_tooltip.js is included first
if(typeof config == "undefined")
	alert("Error:\nThe core tooltip script file 'wz_tooltip.js' must be included first, before the plugin files!");

// Here we define new global configuration variable(s) (as members of the
// predefined "config." class).
// From each of these config variables, wz_tooltip.js will automatically derive
// a command which can be passed to Tip() or TagToTip() in order to customize
// tooltips individually. These command names are just the config variable
// name(s) translated to uppercase,
// e.g. from config. CenterWindow a command CENTERWINDOW will automatically be
// created.

//===================  GLOBAL TOOPTIP CONFIGURATION  =========================//
config. CenterWindow = false	// true or false - set to true if you want this to be the default behaviour
config. CenterAlways = false	// true or false - recenter if window is resized or scrolled
//=======  END OF TOOLTIP CONFIG, DO NOT CHANGE ANYTHING BELOW  ==============//


// Create a new tt_Extension object (make sure that the name of that object,
// here ctrwnd, is unique amongst the extensions available for
// wz_tooltips.js):
var ctrwnd = new tt_Extension();

// Implement extension eventhandlers on which our extension should react
ctrwnd.OnLoadConfig = function()
{
	if(tt_aV[CENTERWINDOW])
	{
		// Permit CENTERWINDOW only if the tooltip is sticky
		if(tt_aV[STICKY])
		{
			if(tt_aV[CENTERALWAYS])
			{
				// IE doesn't support style.position "fixed"
				if(tt_ie)
					tt_AddEvtFnc(window, "scroll", Ctrwnd_DoCenter);
				else
					tt_aElt[0].style.position = "fixed";
				tt_AddEvtFnc(window, "resize", Ctrwnd_DoCenter);
			}
			return true;
		}
		tt_aV[CENTERWINDOW] = false;
	}
	return false;
};
// We react on the first OnMouseMove event to center the tip on that occasion
ctrwnd.OnMoveBefore = Ctrwnd_DoCenter;
ctrwnd.OnKill = function()
{
	if(tt_aV[CENTERWINDOW] && tt_aV[CENTERALWAYS])
	{
		tt_RemEvtFnc(window, "resize", Ctrwnd_DoCenter);
		if(tt_ie)
			tt_RemEvtFnc(window, "scroll", Ctrwnd_DoCenter);
		else
			tt_aElt[0].style.position = "absolute";
	}
	return false;
};
// Helper function
function Ctrwnd_DoCenter()
{
	if(tt_aV[CENTERWINDOW])
	{
		var x, y, dx, dy;

		// Here we use some functions and variables (tt_w, tt_h) which the
		// extension API of wz_tooltip.js provides for us
		if(tt_ie || !tt_aV[CENTERALWAYS])
		{
			dx = tt_GetScrollX();
			dy = tt_GetScrollY();
		}
		else
		{
			dx = 0;
			dy = 0;
		}
		// Position the tip, offset from the center by OFFSETX and OFFSETY
		x = (tt_GetClientW() - tt_w) / 2 + dx + tt_aV[OFFSETX];
		y = (tt_GetClientH() - tt_h) / 2 + dy + tt_aV[OFFSETY];
		tt_SetTipPos(x, y);
		return true;
	}
	return false;
}
