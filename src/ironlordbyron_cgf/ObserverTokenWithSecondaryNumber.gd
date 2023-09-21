extends Token

export var SecondaryNumber := 0 setget set_secondary_number, get_secondary_number

onready var count_label_2 = $CenterContainer/Count2 # Assuming the node path is correct

func _ready():
	._ready()
	set_secondary_label(SecondaryNumber)

func set_secondary_number(value : int) -> void:
	SecondaryNumber = value
	# Here you might want to add logic to update anything dependent on SecondaryNumber

func get_secondary_number() -> int:
	return SecondaryNumber

func set_secondary_label(value : int) -> void:
	count_label_2.text = str(value)
