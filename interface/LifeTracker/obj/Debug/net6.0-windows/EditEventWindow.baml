   M S B A M L   `   `   `  ????F  BLifeTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:LifeTrackerLifeTracker  5   	   !    LifeTracker.EditEventWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:LifeTracker     5   	     ? Title$  EditEventWindow??5   	   $	??450??6!   $	??370??6.   .??5	      ? ???	#E8E5FFE4$
??Auto??6"   $??0,0,0,-6q?6/    ? RowDefinitions 5
   
   ?? 5      $	??33* ?6   ?? 5      $	??45* ?6   ?? 5      $	??44* ?6   .+?5   
   ?? -   5         ??Name$ 
TitleInput??5      $
??Left=?6&   $??	84,10,0,0q?6A    ?? TextWrapping$
 Wrap=?6T   $	??Top=?6h   $	??254??6?   ?? 5   
   $
??Left=?6   $??	12,12,0,0q?6/   $
??Wrap=?6B   $??Title:??6V   $	??Top=?6d   ?? 5   
   $
??Left=?6   $??	12,37,0,0q?6/   $
??Wrap=?6B   $	??Top=?6V   ?? 6o   $?Time:??6s   ?? 6?   ?? 6?   6?   ?? 5   
   $
??Left=?6   $??	12,66,0,0q?6/   $
??Wrap=?6B   $??Date:??6V   $	??Top=?6c   ?? 5   
   $
??Left=?6   $??12,3,0,0q?6/   $
??Wrap=?6A   $??	Priority:??6U   $	??Top=?6f   $??1??6~   ?? 5   
   $
??Left=?6   $??	12,33,0,0q?6/   $
??Wrap=?6B   $??Flexibility:??6V   $	??Top=?6j   $??1??6?   ?? 5   
   $
??Left=?6   $??	12,65,0,0q?6/   $
??Wrap=?6B   $??	Location:??6V   $	??Top=?6g   $??1??6   ?? 5   
   $
??Left=?6   $??	12,94,0,0q?6/   $
??Wrap=?6B   $??Description:??6V   $	??Top=?6j   $??1??6?   ?? 5   
   $
??Left=?6   $??	12,48,0,0q?6/   $
??Wrap=?6B   $??Color:??6V   $	??Top=?6d   $??2??6|   ?? 5   
   -   5        $ LocationInput??5      $
??Left=?6)   $??	84,64,0,0q?6D   $
 Wrap=?6W   $	??Top=?6k   $	??254??6?   $??1??6?   ?? 5   
   -   5        $ DescriptionInput??5      $
??Left=?6,   $??	84,94,0,0q?6G   $
 Wrap=?6Z   $	??Top=?6n   $	??254??6?   $??75??6?   $??2??6?   $??1??6?   ?? 5   
   -   5         ??Name$ 	TimeList1??5      $
??Left=?6&   $??	84,35,0,0q?6A   $	??Top=?6T   $??65??6l   $??21??6w   ?? 5   
   $
??Left=?6   $??
208,28,0,0q?6/   $
??Wrap=?6C   $??-??6W   $	??Top=?6`   $??22(?6x   $??29??6?   ?? 5   
   -   5        $ AMPM1??5      $
??Left=?6"   $??
154,35,0,0q?6=   $	??Top=?6Q   $??48??6i   $??21??6t   ?? 5   
   -   5        $ 	TimeList2??5      $
??Left=?6&   $??
221,35,0,0q?6A   $	??Top=?6U   $??63??6m   $??21??6x   ?? 5   
   -   5        $ AMPM2??5      $
??Left=?6"   $??
289,35,0,0q?6=   $	??Top=?6Q   $??49??6i   $??21??6t   ?? 5   
   -   5        $ 	MonthList??5      $
??Left=?6&   $??	84,63,0,0q?6A   $	??Top=?6T   $??76??6l   $??22??6w   ?? 5    
   -	   5        $ DayList??5       $
??Left=?6$   $??
165,61,0,0q?6?   $	??Top=?6S   $??52??6k   $??24??6v   ?? 5!   
   -
   5        $ YearList??5!      $
??Left=?6%   $??
222,62,0,0q?6@   $	??Top=?6T   $??73??6l   $??24??6w   ?? 5"   
   -   5        $
??Left=?5"      $??32??6/   $??	48,94,0,0q?6;   
???   ?$	??Top=?6]   $	??100??6u   ???	#FFB4DCB3 ?? RadiusX$ 10??6?    ?? RadiusY$ 10??6?   $??2??6?   6?   ?? 5#   
   -   5        $
??Left=?5#      $??	74,98,0,0q?6/   $??2??6B   $
??Wrap=?6O   $
??OKAY??6c   $	??Top=?6o   $??20(?6?   $??Cambria)?6?   6?   ?? 5$   
   -   5        $ 	ColorList??5$      $
??Left=?6&   $??	84,46,0,0q?6A   $	??Top=?6T   $??76??6l   $??20??6w   $??2??6?   ?? 5%   
   -   5        $ FlexibilityList??5%      $
??Left=?6,   $??	84,32,0,0q?6G   $	??Top=?6Z   $??76??6r   $??22??6}   $??1??6?   5&      ?? 5'   
   -   5        $ PriorityList??5'      $
??Left=?6)   $??84,1,0,0q?6D   $	??Top=?6V   $??76??6n   $??22??6y   $??1??6?   5(      ?? 5)   
   -   5        $
??Left=?5)      $??32??6/   $??
196,95,0,0q?6;   
???   ?$	??Top=?6^   $	??100??6v   ???	#FFE79E8E$ 10??6?   $ 10??6?   $??2??6?   6?   ?? 5*   
   -   5        $
??Left=?5*      $??211,100,0,0q?6/   $??2??6D   $
??Wrap=?6Q   $??DELETE??6e   $	??Top=?6s   $??20(?6?   $??Cambria)?6?   6?   5+      5,      