Please help me! #speaker:Crystal? #portrait:default #layout:right
-> main

=== main ===
Im trapped by the dark forces of this land! Will you please help me? #speaker:Crystal? #portrait:default #layout:right
* [Where am I?] Where am I and how the hell did I get here?! #speaker:Mr. T.Inker #portrait:player #layout:left
    
    Why this is the land of Unravel of course! #speaker:Crystal? #portrait:default #layout:right
        
        ** I need more answers... #speaker:Mr. T.Inker #portrait:player #layout:left  
            -> main
    
* [Who are you?] I can't believe I am talking to a crystal?! Who are you? #speaker:Mr. T.Inker #portrait:player #layout:left

    Why that is a long story dear traveller... #speaker:Crystal? #portrait:default #layout:right
    -> known

* [No] I have enough on my plate dear crystal! #speaker:Mr. T.Inker #portrait:player #layout:left
    -> endUnknown

=== known ===
My name is Princess Fairyela dear one. My father is the sole ruler and protecter of these lands. #speaker:Princess Fairyela #portrait:girl #layout:right

* [How can I help?] Exactly what could a simple tinkerer possibly do to help with curse like this? #speaker:Mr. T.Inker #portrait:player #layout:left
    -> explanation

=== explanation ===
You will have to protect me against the dark invaders for a 100 days! That is how long it will take to gather my powers and break free of this curse... #speaker:Princess Fairyela #portrait:girl #layout:right

* [Count on me!] I will do what is in my power to protect you! #speaker:Mr. T.Inker #portrait:player #layout:left
    -> protect
    
* [I can't!] I am simply not capable of fighting anyone! #speaker:Mr. T.Inker #portrait:player #layout:left
    -> coward

=== protect ===
My hero!!! And I will you aid you with as much of my powers I can gather! #speaker:Princess Fairyela #portrait:girl #layout:right
    -> endKnownProtect

=== endKnownProtect
    -> END

=== coward ===
Then I and all that is good in this land will perish... #speaker:Princess Fairyela #portrait:girl #layout:right
    -> endKnownCoward

=== endKnownCoward ===
-> END

=== endUnknown ===
Please dear one! #speaker:Crystal? #portrait:default #layout:right
-> END
