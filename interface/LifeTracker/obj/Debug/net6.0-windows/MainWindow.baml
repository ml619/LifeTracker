   M S B A M L   `   `   `  ????F  BLifeTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:LifeTrackerLifeTracker  5   	       LifeTracker.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:LifeTracker     5   	     ? Title$  
MainWindow??5   	   $
??Auto??6   $
??Auto??6*    ? SizeToContent$ WidthAndHeight=?67    ? WindowState$ 	Maximized=?6V   .??5	      (?  (? Stretch$
 Fill=?6   .??5
   
   (? $ UniformToFill=?6   .??5      ? -   5        $??MainBackground??5      ???	#E8E5FFE4$	??453??6@   $	??821??6M    ? RowDefinitions 5      ?? 5      $	??63* ?6$   ?? 5      $	??88* ?6$   .+?5      ?? $
??Left=?6   $	??386??67   $??	80,57,0,0q?6D   
???   ?$	??Top=?6f   $	??731??6~    ?? RadiusX$ 10??6?    ?? RadiusY$ 10??6?   ???	#FFEBEBEB$??2??6?   ?? 5      $
??Left=?6   $??44??67   $??80,8,0,0q?6C   
???   ?$	??Top=?6d   $	??710??6|   $ 15??6?   $ 15??6?   ???	#FFB4DCB36?   ?? 5      $
??Left=?6   $	??386??67   $??	10,57,0,0q?6D   
???   ?$	??Top=?6f   $??65??6~   $ 10??6?   $ 10??6?   ???	#FFEBEBEB$??2??6?   ?? 5      $
??Left=?6   $??50??67   $??	87,62,0,0q?6C   
???   ?$	??Top=?6e   $??95??6}   $ 10??6?   $ 10??6?   ???	#FFB4DCB36?   ?? 5      $
??Left=?6   $??50??67   $??
187,62,0,0q?6C   
???   ?$	??Top=?6f   $??95??6~   $ 10??6?   $ 10??6?   ???	#FFB4DCB36?   ?? 5      $
??Left=?6   $??50??67   $??
287,62,0,0q?6C   
???   ?$	??Top=?6f   $??95??6~   $ 10??6?   $ 10??6?   ???	#FFB4DCB36?   ?? 5      $
??Left=?6   $??50??67   $??
386,62,0,0q?6C   
???   ?$	??Top=?6f   $??96??6~   $ 10??6?   $ 10??6?   ???	#FFB4DCB36?   ?? 5      $
??Left=?6   $??50??67   $??
486,62,0,0q?6C   
???   ?$	??Top=?6f   $??96??6~   $ 10??6?   $ 10??6?   ???	#FFB4DCB36?   ?? 5      $
??Left=?6   $??50??67   $??
586,62,0,0q?6C   
???   ?$	??Top=?6f   $??96??6~   $ 10??6?   $ 10??6?   ???	#FFB4DCB36?   ?? 5      $
??Left=?6   $??50??67   $??
686,62,0,0q?6C   
???   ?$	??Top=?6f   $??96??6~   $ 10??6?   $ 10??6?   ???	#FFB4DCB36?   ?? 5      $
??Left=?6   $??	10,20,0,0q?67   $
??Wrap=?6J   $??LifeTracker??6^   $	??Top=?6q   $??22??6?   $??65??6?    ?? TextAlignment$ Center=?6?   $??19(?6?   $??Freestyle Script)?6?   $
??Bold"?6?    ?? LineStackingStrategy$ 	MaxHeight=?6?   $??Normal&?6  	 ?? 
LineHeight$	 16??6+  ?? 5      -   5        $	??Add??5      $
??Left=?6'   $??	15,74,0,0q?6B   $	??Top=?6U   $??55??6m   $??38??6x   ???	#FFB4DCB3
???   ?
???   ?$??Cambria)?6?   $??9(?6?   $
??Bold"?6?   6?   ?? 5      -   5        $??
Reschedule??5      $
??Left=?6.   $??
15,125,0,0q?6I   $	??Top=?6]   $??55??6u   $??38??6?   ???	#FFB4DCB3
???   ?
???   ?$??Cambria)?6?   $??9(?6?   $
??Bold"?6?   6?   ?? 5      -   5        $??Availability??5      $
??Left=?60   $??
15,176,0,0q?6K   $	??Top=?6_   $??55??6w   $??38??6?   ???	#FFB4DCB3
???   ?
???   ?$??Cambria)?6?   $??9(?6?   $
??Bold"?6?   $??2??6   6  ?? 5      $
??Left=?6   $??	87,65,0,0q?67   $
??Wrap=?6J   $??Monday??6^   $	??Top=?6l   $ Center=?6?   $??95??6?   $??13(?6?   $
??Bold"?6?   $??Cambria)?6?   ?? 5      $
??Left=?6   $??
187,65,0,0q?67   $
??Wrap=?6K   $??Tuesday??6_   $	??Top=?6n   $ Center=?6?   $??95??6?   $??13(?6?   $
??Bold"?6?   $??Cambria)?6?   ?? 5       $
??Left=?6   $??
287,65,0,0q?67   $
??Wrap=?6K   $??	Wednesday??6_   $	??Top=?6p   $ Center=?6?   $??94??6?   $??13(?6?   $
??Bold"?6?   $??Cambria)?6?   ?? 5!      $
??Left=?6   $??
387,65,0,0q?67   $
??Wrap=?6K   $??Thursday??6_   $	??Top=?6o   $ Center=?6?   $??94??6?   $??13(?6?   $
??Bold"?6?   $??Cambria)?6?   ?? 5"      $
??Left=?6   $??
487,65,0,0q?67   $
??Wrap=?6K   $??Friday??6_   $	??Top=?6m   $ Center=?6?   $??94??6?   $??13(?6?   $
??Bold"?6?   $??Cambria)?6?   ?? 5#      $
??Left=?6   $??
587,65,0,0q?67   $
??Wrap=?6K   $??Saturday??6_   $	??Top=?6o   $ Center=?6?   $??94??6?   $??13(?6?   $
??Bold"?6?   $??Cambria)?6?   ?? 5$      $
??Left=?6   $??
687,65,0,0q?67   $
??Wrap=?6K   $??Sunday??6_   $	??Top=?6m   $ Center=?6?   $??95??6?   $??13(?6?   $
??Bold"?6?   $??Cambria)?6?   ?? 5%      -   5        $??Save & Exit??5%      $
??Left=?63   $??
17,204,0,0q?6N   $	??Top=?6b   $??51??6z   $??20??6?   ???	#FFF39A9A
???   ?
???   ?$??Cambria)?6?   $??9(?6?   $
??Bold"?6?   $??1??6  ?? 5&      -   5        $??
MuteButton??5&   (   $
??Mute??6   $
??Left=?6:   $??
17,160,0,0q?6U   $	??Top=?6i   $??51??6?   $??20??6?   ???	#FFB4DCB3
???   ?
???   ?$??Cambria)?6?   $??9(?6?   $
??Bold"?6?   $??1??6"  ?? 5'      -   5        $??Minimize??5'      $
??Left=?6,   $??
17,182,0,0q?6G   $	??Top=?6[   $??51??6s   $??20??6~   ???	#FFFFEFA0
???   ?
???   ?$??Cambria)?6?   $??9(?6?   $
??Bold"?6?   $??1??6  (  "System.Windows.Controls.DatePicker  5(      -   5        $??SelectDisplayWeek??5(   ?   $
??Left=?6P   $??
100,18,0,0q?6k   $	??Top=?6   6?   ?? 5)      -	   5        $??
MondayText??5)      $
??Left=?6.   $??	110,0,0,0q?6I   $
??Wrap=?6\   $??Center=?6p   ?? 5*      -
   5        $??TuesdayText??5*      $
??Left=?6/   $??	202,0,0,0q?6J   $
??Wrap=?6]   $??Center=?6q   ?? 5+      -   5        
 ??Name$
 
mondayText??5+      $
??Left=?60   $??	120,0,0,0q?6K   $
??Wrap=?6^   $??	TextBlock??6r   $??Center=?6?   $ Center=?6?   ?? 5,      -   5        $
 tuesdayText??5,      $
??Left=?61   $??	220,0,0,0q?6L   $
??Wrap=?6_   $??	TextBlock??6s   $??Center=?6?   $ Center=?6?   ?? 5-      -   5        $
 wednesdayText??5-      $
??Left=?63   $??	320,0,0,0q?6N   $
??Wrap=?6a   $??	TextBlock??6u   $??Center=?6?   $ Center=?6?   ?? 5.      -   5        $
 thursdayText??5.      $
??Left=?62   $??	419,0,0,0q?6M   $
??Wrap=?6`   $??	TextBlock??6t   $??Center=?6?   $ Center=?6?   ?? 5/      -   5        $
 
fridayText??5/      $
??Left=?60   $??	519,0,0,0q?6K   $
??Wrap=?6^   $??	TextBlock??6r   $??Center=?6?   $ Center=?6?   ?? 50      -   5        $
 saturdayText??50      $
??Left=?62   $??	619,0,0,0q?6M   $
??Wrap=?6`   $??	TextBlock??6t   $??Center=?6?   $ Center=?6?   ?? 51      -   5        $
 
sundayText??51      $
??Left=?60   $??	720,0,0,0q?6K   $
??Wrap=?6^   $??	TextBlock??6r   $??Center=?6?   $ Center=?6?   ?? 52      -   5        $
 weekSpanText??52      $
??Left=?62   $??
308,15,0,0q?6M   $
??Wrap=?6a   $??December 12 - December 12??6u   $	??Top=?6?   $??22(?6?   $ Center=?6?   ? 53      -   5         ?Name$ weekForward??53       ? Points ?    $? ???  -1
???   ?
???   ?$??617,20,187,149q?6d   6|   ? 54      -   5        $ weekBack??54       ?    $? ???  $?  -1
???   ?
???   ?$??265,21,539,148q?6b   6z   ?? 55      -   5        $
 yearText??55      $
??Left=?6.   $??
697,15,0,0q?6I   $
??Wrap=?6]   $
??2022??6q   $	??Top=?6}   $??22(?6?   $ Center=?6?   $??69??6?   ?? 56      $
??Auto=?6   $??82,115,10,23q?6D   $??2??6Z   .??57      ?? $	??Top=?6!   $
??Left=?69   $??0,0,0,0q?6T   .+?58      ? -   5        $??Scroll_Area??58      $??0,0,0,0q?62   $	??768??6C   .+?59      ?? -   5         ??Name$ 	mondayCol??59   (   $
??Left=?6;   $	??768??6V   $??6,11,0,0q?6c   
???   ?$	??Top=?6?   $??95??6?   $ 10??6?   $ 10??6?   
???????6?   ?? 5:      -   5        $ 
tuesdayCol??5:   (   $
??Left=?6<   $	??768??6W   $??
106,11,0,0q?6d   
???   ?$	??Top=?6?   $??95??6?   $ 10??6?   $ 10??6?   
???????6?   ?? 5;      -   5        $ wednesdayCol??5;   (   $
??Left=?6>   $	??768??6Y   $??
206,11,0,0q?6f   
???   ?$	??Top=?6?   $??95??6?   $ 10??6?   $ 10??6?   
???????6?   ?? 5<      -   5        $ thursdayCol??5<   (   $
??Left=?6=   $	??768??6X   $??
307,11,0,0q?6e   
???   ?$	??Top=?6?   $??94??6?   $ 10??6?   $ 10??6?   
???????6?   ?? 5=      -   5        $ 	fridayCol??5=   (   $
??Left=?6;   $	??768??6V   $??
406,11,0,0q?6c   
???   ?$	??Top=?6?   $??95??6?   $ 10??6?   $ 10??6?   
???????6?   ?? 5>      -   5        $ saturdayCol??5>   (   $
??Left=?6=   $	??768??6X   $??
506,11,0,0q?6e   
???   ?$	??Top=?6?   $??95??6?   $ 10??6?   $ 10??6?   
???????6?   ?? 5?      -   5        $ 	sundayCol??5?   (   $
??Left=?6;   $	??768??6V   $??
606,11,0,0q?6c   
???   ?$	??Top=?6?   $??95??6?   $ 10??6?   $ 10??6?   
???????6?   ?? 5@      -   5        $
 text12am??5@   (   $
??Left=?6:   $??	39,11,0,0q?6U   $
??Wrap=?6h   $????12am                     12am                     12am                       12am                    12am                      12am                     12am??6|   $	??Top=?6   $	??652??68  ???	#FFE5E5E56D  ?? 5A      -   5        $
 text1am??5A   (   $
??Left=?69   $??	39,43,0,0q?6T   $
??Wrap=?6g   $???? 1am                      1am                       1am                         1am                      1am                        1am                       1am??6{   $	??Top=?6$  $	??652??6<  ???	#FFE5E5E56H  ?? 5B      -    5        $
 text2am??5B   (   $
??Left=?69   $??	39,75,0,0q?6T   $
??Wrap=?6g   $???? 2am                      2am                       2am                         2am                      2am                        2am                       2am??6{   $	??Top=?6$  $	??652??6<  ???	#FFE5E5E56H  ?? 5C      -!   5        $
 text3am??5C   (   $
??Left=?69   $??
39,107,0,0q?6T   $
??Wrap=?6h   $???? 3am                      3am                       3am                         3am                      3am                        3am                       3am??6|   $	??Top=?6%  $	??652??6=  ???	#FFE5E5E56I  ?? 5D      -"   5        $
 text4am??5D   (   $
??Left=?69   $??
39,139,0,0q?6T   $
??Wrap=?6h   $???? 4am                      4am                       4am                         4am                      4am                        4am                       4am??6|   $	??Top=?6%  $	??652??6=  ???	#FFE5E5E56I  ?? 5E      -#   5        $
 text5am??5E   (   $
??Left=?69   $??
39,171,0,0q?6T   $
??Wrap=?6h   $???? 5am                      5am                       5am                         5am                      5am                        5am                       5am??6|   $	??Top=?6%  $	??652??6=  ???	#FFE5E5E56I  ?? 5F      -$   5        $
 text6am??5F   (   $
??Left=?69   $??
39,203,0,0q?6T   $
??Wrap=?6h   $???? 6am                      6am                       6am                         6am                      6am                        6am                       6am??6|   $	??Top=?6%  $	??652??6=  ???	#FFE5E5E56I  ?? 5G      -%   5        $
 text7am??5G   (   $
??Left=?69   $??
39,235,0,0q?6T   $
??Wrap=?6h   $???? 7am                      7am                       7am                         7am                      7am                        7am                       7am??6|   $	??Top=?6%  $	??652??6=  ???	#FFE5E5E56I  ?? 5H      -&   5        $
 text8am??5H   (   $
??Left=?69   $??
39,267,0,0q?6T   $
??Wrap=?6h   $???? 8am                      8am                       8am                         8am                      8am                        8am                       8am??6|   $	??Top=?6%  $	??652??6=  ???	#FFE5E5E56I  ?? 5I      -'   5        $
 text9am??5I   (   $
??Left=?69   $??
39,299,0,0q?6T   $
??Wrap=?6h   $???? 9am                      9am                       9am                         9am                      9am                        9am                       9am??6|   $	??Top=?6%  $	??652??6=  ???	#FFE5E5E56I  ?? 5J      -(   5        $
 text10am??5J   (   $
??Left=?6:   $??
39,331,0,0q?6U   $
??Wrap=?6i   $????10am                     10am                     10am                       10am                    10am                      10am                     10am??6}   $	??Top=?6!  $	??652??69  ???	#FFE5E5E56E  ?? 5K      -)   5        $
 text11am??5K   (   $
??Left=?6:   $??
39,363,0,0q?6U   $
??Wrap=?6i   $????11am                     11am                     11am                       11am                    11am                      11am                     11am??6}   $	??Top=?6!  $	??652??69  ???	#FFE5E5E56E  ?? 5L      -*   5        $
 text12pm??5L   (   $
??Left=?6:   $??
39,395,0,0q?6U   $
??Wrap=?6i   $????12pm                     12pm                     12pm                       12pm                    12pm                      12pm                     12pm??6}   $	??Top=?6!  $	??652??69  ???	#FFE5E5E56E  ?? 5M      -+   5        $
 text1pm??5M   (   $
??Left=?69   $??
39,427,0,0q?6T   $
??Wrap=?6h   $???? 1pm                       1pm                       1pm                         1pm                     1pm                       1pm                      1pm??6|   $	??Top=?6#  $	??652??6;  ???	#FFE5E5E56G  ?? 5N      -,   5        $
 text2pm??5N   (   $
??Left=?69   $??
39,459,0,0q?6T   $
??Wrap=?6h   $???? 2pm                       2pm                       2pm                         2pm                     2pm                       2pm                      2pm??6|   $	??Top=?6#  $	??652??6;  ???	#FFE5E5E56G  ?? 5O      --   5        $
 text3pm??5O   (   $
??Left=?69   $??
39,491,0,0q?6T   $
??Wrap=?6h   $???? 3pm                       3pm                       3pm                         3pm                     3pm                       3pm                      3pm??6|   $	??Top=?6#  $	??652??6;  ???	#FFE5E5E56G  ?? 5P      -.   5        $
 text4pm??5P   (   $
??Left=?69   $??
39,523,0,0q?6T   $
??Wrap=?6h   $???? 4pm                       4pm                       4pm                         4pm                     4pm                       4pm                      4pm??6|   $	??Top=?6#  $	??652??6;  ???	#FFE5E5E56G  ?? 5Q      -/   5        $
 text5pm??5Q   (   $
??Left=?69   $??
39,555,0,0q?6T   $
??Wrap=?6h   $???? 5pm                       5pm                       5pm                         5pm                     5pm                       5pm                      5pm??6|   $	??Top=?6#  $	??652??6;  ???	#FFE5E5E56G  ?? 5R      -0   5        $
 text6pm??5R   (   $
??Left=?69   $??
39,587,0,0q?6T   $
??Wrap=?6h   $???? 6pm                       6pm                       6pm                         6pm                     6pm                       6pm                      6pm??6|   $	??Top=?6#  $	??652??6;  ???	#FFE5E5E56G  ?? 5S      -1   5        $
 text7pm??5S   (   $
??Left=?69   $??
39,619,0,0q?6T   $
??Wrap=?6h   $???? 7pm                       7pm                       7pm                         7pm                     7pm                       7pm                      7pm??6|   $	??Top=?6#  $	??652??6;  ???	#FFE5E5E56G  ?? 5T      -2   5        $
 text8pm??5T   (   $
??Left=?69   $??
39,651,0,0q?6T   $
??Wrap=?6h   $???? 8pm                       8pm                       8pm                         8pm                     8pm                       8pm                      8pm??6|   $	??Top=?6#  $	??652??6;  ???	#FFE5E5E56G  ?? 5U      -3   5        $
 text9pm??5U   (   $
??Left=?69   $??
39,683,0,0q?6T   $
??Wrap=?6h   $???? 9pm                       9pm                       9pm                         9pm                     9pm                       9pm                      9pm??6|   $	??Top=?6#  $	??652??6;  ???	#FFE5E5E56G  ?? 5V      -4   5        $
 text10pm??5V   (   $
??Left=?6:   $??
39,715,0,0q?6U   $
??Wrap=?6i   $????10pm                     10pm                    10pm                      10pm                     10pm                     10pm                     10pm??6}   $	??Top=?6  $	??652??67  ???	#FFE5E5E56C  ?? 5W      -5   5        $
 text11pm??5W   (   $
??Left=?6:   $??
39,748,0,0q?6U   $
??Wrap=?6i   $????11pm                     11pm                    11pm                      11pm                     11pm                     11pm                     11pm??6}   $	??Top=?6  $	??652??67  ???	#FFE5E5E56C  ?? 5X      -6   5        $ AcceptSuggestionButton??5X   (    K? ZIndex$ 2??6H   $
??Left=?6X   $??21??6s   $??	51,19,0,0q?6   ???	#FF086C19$	??Top=?6?   $??21??6?   ???	#FF2DFF00$ 15??6?   $ 12??6?   $??2??6?   6  ?? 5Y      -7   5        $ RejectSuggestionButton??5Y   (   $ 2??6H   $
??Left=?6X   $??21??6s   $??	77,19,0,0q?6   ???	#FF6C0808$	??Top=?6?   $??21??6?   
???  ??$ 15??6?   $ 12??6?   $??2??6?   6  5Z      5[      5\      5]      5^      5_      5`      