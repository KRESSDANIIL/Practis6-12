   M S B A M L   `   `   `  ����F  BPractis6-12, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Practis6_12.WindowsPractis6_12.Windows  5   	   &     Practis6_12.Windows.DriverWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Practis6_12.Windows     5   	     � Title$  DriverWindow��5   	   $	��484��6   $	��965��6+   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��67* �6   �� 5      $	��69* �6   �� 5      $
��303* �6   .+�5   
   �� $��3��6   .@�5      �� $��Заказы��6   .��5      �  5      �� 5      $��8* �6+   �� 5      $	��25* �6+   �� 5      $	��26* �6+   �� 5      $	��53* �6+   �� 5      $
��760* �6+    � RowDefinitions 5      �� 5      $
��Auto �6(   �� 5      $��* �6(   .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��5,5,5,5q�6:   $��5��6K   .+�5       �� -   5        $��Обновить��5    !   $	��100��64   $��5q�6@   6K   5!      &   System.Windows.Controls.DataGrid  5$      -   5        $��
OrdersGrid��5$   ,   $��1��6      AutoGenerateColumns$ False��6>   $��1,1,1,1q�6y   $��5��6�      
IsReadOnly$
 True��5%         CanUserAddRows$ False��5&         CanUserDeleteRows$ False��5'         CanUserReorderColumns$ False��5(      	   CanUserResizeColumns$
	 True��5)      
   CanUserResizeRows$
 False��5*         CanUserSortColumns$ False��5+      ��5$   Z   �� *Orders+   Columns 5,      0  *System.Windows.Controls.DataGridTextColumn  5-      ,  &System.Windows.Controls.DataGridColumn   Header$ Pickup_location��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6n   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   �� *Pickup_location+6n     5.      $ Dropoff_location��61   $ * 6p    6K   �� *Dropoff_location+6p     5/      $ Status��61   $ * 6\    6A   �� *Status+6\     50      $
 Fare��61   $ * 6X    6?   �� *Fare+6X     51      $ 
Created_at��61   $ * 6d    6E   �� *
Created_at+6d   4  .System.Windows.Controls.DataGridTemplateColumn  52      $ Принять��65   $
 Auto 6F      CellTemplate 53   "   �� 54   &   .R�55   *   �� -   5        $��Выбрать��55   1   
 � Tag 6i   �� 56   '   58      5;      5<      5=      �� 5>      $��Машина��6   .��5?      � $��10q�6    5@      �� 5A      $��3* �6+    5C      �� 5D      $
��Auto �6(   �� 5E      $��* �6(   �� 5F      $
��Auto �6(   .+�5I      � $��2��6   $��	0,31,10,0q�6/   $��2��6B    5J      �� 5K      $��2* �6/   .+�5O      �� $��1��6%   $	��Top=�65   $��10q�6M   .+�5P      �� $��Модель:��6$   �� 5Q      -   5         ��Name$ ModelNameTextBox��5Q   &   �� 5S      $��Номер:��6$   �� 5T      -   5        $ PlateNumberTextBox��5T   &   �� 5V      $��Год:��6$   �� 5W      -   5        $ YearTextBox��5W   &   5X      5Y      �� 5[      $��2��6!   $��0��6.   $��2��6>   $ 
Horizontal=�6R   $��Center=�6k   $��10q�6�   .+�5\      �� -   5         ��Name$
 Save��5\   !   $��Сохранить��6/   $	��150��6C   $��5q�6O   6Z   5]      5^      5_      5`      5a      5b      