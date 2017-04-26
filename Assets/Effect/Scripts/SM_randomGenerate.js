var minTime:float=5;
var maxTime:float=10;
var nowTimer:float=0;
var clonePrefab:GameObject;
var isClone:boolean=false;
var actualRandom:float = 0;

function Start ()
{
    actualRandom=Random.Range(minTime, maxTime);
    nowTimer=0;
    isClone=false;    
}


function Update () {
    nowTimer+=Time.deltaTime;
    if(!isClone)
    {
        if(nowTimer > actualRandom)
        {
            isClone = true;
            var justCreated:GameObject=Instantiate(clonePrefab);  //create the prefab
            justCreated.name="ClonePrefab";
        }
    }
}