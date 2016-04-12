param([string] $ComputerName, [string] $Type )

$group = [ADSI]('WinNT://'+$ComputerName+'/'+$Type+',group')

@($group.Invoke('members'))|foreach {$_.GetType().InvokeMember('Name','GetProperty',$null,$_,$null)}