Xamarin.Forms.Behaviors
=======================

Xamarin Forms Behaviors is a "tentative" porting of core Blend behaviors infrastructure to Xamarin Forms Platform

Read about how to use this package here: http://bit.ly/xamarinbehaviors

What's new in v 1.1.0: http://codeworks.it/blog/?p=216 (introduces Relative Commanding in EventToCommand behavior)

*** Version 1.2.0 ***
In order to have behaviors working in iOS projects you have to add following line into iOS project's AppDelegate class 
just after Xamarin.Forms.Forms.Init() method.

Xamarin.Behaviors.Infrastructure.Init();

For more info why this call is needed read here: http://codeworks.it/blog/?p=242

