   M S B A M L   `   `   `  ����P  LAerolineasRafaelPabon, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AerolineasRafaelPabonAerolineasRafaelPabon  5   	   )    #AerolineasRafaelPabon.AsientoWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:AerolineasRafaelPabon     5   	     � Title$  AsientoWindow��5   	   $	��450��6   $	��800��6,   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $
��Bold"�6]   ��6!   �� *NumeroAsiento+ �� StringFormat$ Asiento: {0}��6]   �� 5   
   $��1��6   ��6!   �� *Tipo+$ 
Clase: {0}���� 5   
   $��2��6   ��6!   �� *Estado+$ Estado: {0}���� 5   
   $��3��6    �� Orientation$ 
Horizontal=�6"   $��0,10,0,0q�6;   .+�5      �� $��
Pasaporte:��6   $��Center=�6*   $��0,0,5,0q�6E   �� 5      -   5         ��Name$ txtPasaporte��5      $	��100��6,   68   5      �� 5   
   -   5         ��Name$ txtNombrePasajero��5   !   $��4��6   $��0,5,0,0q�6<   �� 5"   
   $��5��6   $ 
Horizontal=�6"   $��Center=�6;   $��Bottom=�6X   .+�5#      �� -   5         ��Name$ 
btnComprar��5#      $��Comprar��6)   $��5q�6;   $��80��6F   6Q   �� 5$      -   5        $ btnReservar��5$      $��Reservar��6*   $��5q�6=   $��80��6H   6S   �� 5%      -   5        $ btnDevolver��5%      $��Devolver��6*   $��5q�6=   $��80��6H   6S   5&      �� 5(   
   -   5        $��6��5(      $��Cancelar��6   $��Right=�61   $��80��6M   6X   5)      5*      