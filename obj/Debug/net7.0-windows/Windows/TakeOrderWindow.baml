   M S B A M L   `   `   `  ����F  BPractis6-12, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Practis6_12.WindowsPractis6_12.Windows  5   	   )    #Practis6_12.Windows.TakeOrderWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Practis6_12.Windows     5   	     � Title$  TakeOrderWindow��5   	   $	��450��6!   $	��800��6.   .��5	      � $��10q�6    � ColumnDefinitions 5
   
   �� 5      $��3* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��2��6   $��	0,31,10,0q�6#   $��2��66    5      �� 5      $��2* �6#   .+�5      �� $��1��6   $	��Top=�6)   $��10q�6A   .+�5      �� $-��'Адрес начала поездки:��6   �� 5      -   5         ��Name$ StartLocation��5       �� 
IsReadOnly$
 True��61   �� 5      $"��Конечный адрес:��6   �� 5      -   5        $ EndLocation��5      $
 True��6/   �� 5       $#��Телефон клиента��6   �� 5!      -   5        $ ClientPhone��5!      $
 True��6/   5#      5$      �� 5&   
   $��2��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $��10q�6|   .+�5'      �� -   5         ��Name$ Создать��5'      $��Закончить��6&   $	��150��6:   $��5q�6F   6Q   5(      5)      5*      