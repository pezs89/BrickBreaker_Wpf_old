   M S B A M L   `   `   `  ����  BrickBreaker    BrickBreaker.HighScores   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  
HighScores��5   	   $	��500��6   $
��1000��6)    � WindowStartupLocation$ CenterScreen=�66    � 
ResizeMode$ NoResize=�6t   ��5      �� 5   
    �� ImageSource$! Resources/Concrete_wall.jpg��6   .��5      � $��5q�6    � RowDefinitions 5	   
   �� 5
      �� 5      $��80 �6   .+�5   
   �� 
������ .+�5      &   System.Windows.Controls.DataGrid  -   5        $��	dataGrid1��5         GridLinesVisibility$
 None=�5         AutoGenerateColumns .  $
��Auto��5   -   $
��Bold"�5      $��20(�5      $��10q�5      $��Center=�5      $��Center=�5      
������    RowBackground
 ���� {�.     HeadersVisibility$ Column=�5      $��0q�5      ��5      �� 	 �� Path$	 Elements[Data]�
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��5          
Foreground
 �����1  +System.Windows.Controls.DataGridBoundColumn   Binding 5#      �� $	 Element[Name].Value�   ElementStyle 5$      �� 5%       �� 
TargetType" ��6$   .�5&   "   ��  �� Property � �� �� Value$ Center=�6H   5'      5*        5,      $ Score��5-      
 ����� 5/      �� $	 Element[Score].Value� 50      �� 51      " ��6$   .�52   "   ��  � ��$ Center=�6H   53      56         ColumnHeaderStyle 59      �� 5:      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5;      ��  � ��$
 Bold"�68   �� 5<       � ��
 ���� 67   �� 5=       � ��
 �����67   �� 5>       � ��$ 0q�6<   �� 5?       � ��$ Center=�6@   �� 5@       � ��$ Center=�6G   �� 5A       � ��$ 20q�63   5B      5D      5E      �� 5G   
   -   5         ��Name$ PressKeyTxtBlock��5G      $��1��5H      $��10q�5I      $��Center=�5J      $��Center=�5K      
�������$
��Bold"�5M      $��20(�5N      5Q      5R      