   M S B A M L   `   `   `  ����P  LAerolineasRafaelPabon, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AerolineasRafaelPabonAerolineasRafaelPabon  5   	   &     AerolineasRafaelPabon.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:AerolineasRafaelPabon     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5
         AsientoButtonStyle �� 
TargetType �� Property �� Value �� Triggers �� Binding �� Value��%Y  &	        �� 5   
   " ��6+   .�5      ��  � ��
 ����6+   �� 5       � ��
 �   �6+    5      �� 5       6   �� *Estado+	 Libre69   .a�5      ��  � ��
 ��ح�63   5      �� 5       6   �� *Estado+ 	Reservado69   .a�5      ��  � ��
 � ���63   5      �� 5       6   �� *Estado+ Vendido69   .a�5      ��  � ��
 ����63   �� 5       � {�$ False��62   5      �� 5       6   �� *Estado+ 
Devolucion69   .a�5      ��  � ��
 �����63   5      5      5      .��5       �  � RowDefinitions 5!   
   �� 5"      $
��Auto �6   �� 5#      $��* �6   .+�5'   
   �� $��0��6   	 �� Orientation$	 
Horizontal=�6"   ���	#FF0F4C81.+�5(      �� $��Estoy comprando desde��6   
�������$��14(�6H   $��0,0,10,0q�6V   �� 5)      -   5        
 ��Name$
 cbUbicacion��5)      $	��200��6,    �� SelectedValuePath$ Content��68   .@�5*      �� .��6   Bogotá, Colombia61   �� 5+      .��6   Caracas, Venezuela63   �� 5,      .��6   
Miami, USA6+   5-      �� 5.      -   5         ��Name$ txtHoraActual��5.      
�������$��14(�6B   $��20,0,0,0q�6P   �� 5/      $��Aerolineas Rafael Pabon��6   
�������$��16(�6J   $��	100,0,0,0q�6X   50      � 53   
   $��1��6    � ColumnDefinitions 54      �� 55      $	��250 �6#   �� 56      $��* �6#   .+�5:      �� $��0��6   ���	#FFE1E1E1.+�5;      �� $��Buscar Vuelo��6   $
��Bold"�60   $��0,0,0,10q�6B   �� 5=      $��Origen:��6   �� 5>      -   5        $
 cbOrigen��5>       �� DisplayMemberPath$ ShortDescription��6-   �� 5@      $��Destino:��6   $��0,10,0,0q�6,   �� 5A      -   5        $
 	cbDestino��5A      $ ShortDescription��6.   �� 5C      $��Fecha:��6   $��0,10,0,0q�6*   (  "System.Windows.Controls.DatePicker  5D      -   5          Name$ dpFecha��5D      �� 5F      -   5         ��Name$ 	btnBuscar��5F      $��Buscar Vuelos��6,   $��0,20,0,0q�6D   ���	#FF0F4C81
�������5G   H   5H      �� 5K      $��1��6   .@�5L      �� $��Vuelos Disponibles��6   .��5M      &   System.Windows.Controls.DataGrid  -   5          Name$ dgVuelos��5M         AutoGenerateColumns$ False��61      
IsReadOnly$
 True��6M      Columns 5O      0  *System.Windows.Controls.DataGridTextColumn  5P      ,  &System.Windows.Controls.DataGridColumn   Header$ Código��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *CodigoVuelo+  5Q      $ Origen��61    6A   �� *Ruta.Origen.ShortDescription+  5R      $ Destino��61    6B   �� *Ruta.Destino.ShortDescription+  5S      $ Fecha��61    6@   �� *FechaSalida+ �� StringFormat$ 
dd/MM/yyyy��  5T      $ Hora Salida��61    6F   �� *
HoraSalida+  5U      $ Hora Llegada��61    6G   �� *HoraLlegada+  5V      $ Avión��61    6@   �� *Nave.CodigoNave+5X      5Y      �� 5Z      -   5         ��Name$ tabAsientos��5Z   ,   $��Asientos��6   .��5[      �� .��5\      �� -	   5         ��Name$ 
icAsientos��5\   '   ��6;   �� *
Asientos+ �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=���5]      �� 5^   "   .R�5_   &   A�  A� Columns$ 10��62   5a   #   ��5c      �� 5d   "   .R�5e   &   �� -
   5        #��[  5h      $��40��5i      $��40��6    $��2q�6,   ��5e   -   �� *NumeroAsiento+��5f      �� *(&DataContext.AsientoSeleccionadoCommand+ �� RelativeSource ��  �� AncestorType
 Window��5g      �� 5i   ,   5j   #   5l      5m      5n      5o      5p      5q      5r      