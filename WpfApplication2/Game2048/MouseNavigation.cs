using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace UI.Game2048
{
    class MouseNavigation
    {
        private Canvas canvas;

        public MouseNavigation(Canvas canvas)
        {
            this.canvas = canvas;
            onCanvasAttached();
        }

        private void onCanvasAttached()
        {
            Mouse.AddPreviewMouseDownHandler(canvas, OnMouseDown);
            Mouse.AddPreviewMouseMoveHandler(canvas, OnMouseMove);
            Mouse.AddPreviewMouseUpHandler(canvas, OnMouseUp);
            Mouse.AddPreviewMouseWheelHandler(canvas, OnMouseWheel);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isPanning && e.MiddleButton == MouseButtonState.Pressed)
            {
                
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ShouldStartPanning(e))
                StartPanning(e);
        }


        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            
        }

        private bool ShouldStartPanning(MouseButtonEventArgs e)
        {
            return e.ChangedButton == MouseButton.Middle && Keyboard.Modifiers == ModifierKeys.None;
        }

        Point panningStartPointInScreen;
        bool isPanning = false;
        private void StartPanning(MouseButtonEventArgs e)
        {
            
        }




    }
}
