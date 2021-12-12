-> main

=== main ===
Will you defend me from the endless hordes of zombies?
    + [Yes my queen!]
        -> chosen("Yes my queen!")
    + [I will die trying!]
        -> chosen("I will die trying!")
    + [Screw you, god damn thot!]
        -> chosen("Screw you, god damn thot!")

=== chosen(answer) ===
You answered: {answer}
-> END