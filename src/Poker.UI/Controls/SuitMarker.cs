using Avalonia;
using Avalonia.Controls.Primitives;

namespace Poker.UI.Controls;

public class SuitMarker : TemplatedControl
{
    public static readonly StyledProperty<CardSuit> SuitProperty =
        AvaloniaProperty.Register<TemplatedControl, CardSuit>(nameof(Suit));

    public CardSuit Suit
    {
        get => GetValue(SuitProperty);
        set => SetValue(SuitProperty, value);
    }
}