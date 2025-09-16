using Core;
using Messages;
using PlayerInput;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private RoomConfig _roomConfig;
    [SerializeField] private MessageHistoryView _messageHistoryView;
    [SerializeField] private PlayerInputView _playerInputView;
    [SerializeField] private GameVisualEffects _gameVisualEffects;
    [SerializeField] private NextLevelButton _nextLevelButton;
    
    private PlayerInput.PlayerInput _playerInput;
    private MessageHistory _messageHistory;
    private MessageProcessingPipeline _messageProcessingPipeline;
    private RoomProgress _roomProgress;
    private Inventory _inventory;

    public void Run(GameCycle game)
    {
        _inventory = new Inventory();
        _roomProgress = new RoomProgress(_roomConfig);
        _messageHistory = new MessageHistory(15);
        var lookCommandProcessor = new LookCommandProcessor(_roomConfig, _roomProgress, _messageHistory);
        _messageProcessingPipeline = new MessageProcessingPipeline(_messageHistory,
            new IMessageProcessor[]
            {
                lookCommandProcessor,
                new TakeCommandProcessor(_inventory, _roomProgress, _messageHistory),
                new CheckInventoryMessageProcessor(_inventory, _messageHistory),
                new ReadCommandProcessor(_roomConfig, _messageHistory),
                new UnlockCommandProcessor(_roomConfig, _inventory, _messageHistory, game),
                new CraftMessageProcessor(_roomConfig, _inventory, _messageHistory)
            });
        _playerInput = new PlayerInput.PlayerInput(_messageProcessingPipeline);

        _messageHistoryView.Construct(_messageHistory);
        _playerInputView.Construct(_playerInput);
        _nextLevelButton.Construct(game);
        
        lookCommandProcessor.TryProcess(new[] { "look" });
        _gameVisualEffects.Construct(game, _roomConfig);
        game.Start();
    }
}