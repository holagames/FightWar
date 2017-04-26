var warmupTime : float = 10;
var emitters : ParticleEmitter[];
function Start () {
	for (var i = 0; i < warmupTime; i++) {
		for(var x = 0; x < emitters.length; x ++){
    	emitters[x].Simulate(1);
		}
	}
}