// ----------- CAR TUTORIAL-----------------

// These variables allow the script to power the wheels of the car.
var FrontLeftWheel : WheelCollider;
var FrontRightWheel : WheelCollider;

static var hits:int = 5;
static var time:double = 60;

// These variables are for the gears, the array is the list of ratios. The script
// uses the defined gear ratios to determine how much torque to apply to the wheels.
var GearRatio : float[];
var CurrentGear : int = 0;

// These variables are just for applying torque to the wheels and shifting gears.
// using the defined Max and Min Engine RPM, the script can determine what gear the
// car needs to be in.
var EngineTorque : float = 230.0;
var MaxEngineRPM : float = 3000.0;
var MinEngineRPM : float = 1000.0;
private var EngineRPM : float = 0.0;
private var direction:int = 0;


public var timer: GUIText;
public var hit: GUIText;

function Start () {
	rigidbody.centerOfMass += Vector3(0, -1.2, .25);
	time = 60 +  10 * ((5 - PlayerPrefs.GetInt("Lives") + PlayerPrefs.GetInt("Score")) / (Application.levelCount - 2));
	hits = 10 + 8 * ((5 - PlayerPrefs.GetInt("Lives") + PlayerPrefs.GetInt("Score")) / (Application.levelCount - 2));;
    }

function Update () {

	var time2:int = time;

	if(hits <= 0){
		PlayerPrefs.SetInt("Won",1);
		PlayerPrefs.SetInt("Score",PlayerPrefs.GetInt("Score")+1);
		Application.LoadLevel(1);
		
	}
	
	if (time > 0 ){
		timer.text = time2.ToString();
		hit.text = hits.ToString();
	
	}
	else{
		timer.text = "Out of time";
		PlayerPrefs.SetInt("Won",0);
		PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives")-1);
		Application.LoadLevel(1);

	}
	
	time -= 0.1;
	
	// Compute the engine RPM based on the average RPM of the two wheels, then call the shift gear function
	EngineRPM = (FrontLeftWheel.rpm + FrontRightWheel.rpm)/2 * GearRatio[CurrentGear];
	ShiftGears();

	// set the audio pitch to the percentage of RPM to the maximum RPM plus one, this makes the sound play
	// up to twice it's pitch, where it will suddenly drop when it switches gears.
	audio.pitch = Mathf.Abs(EngineRPM / MaxEngineRPM) + 1.0 ;
	// this line is just to ensure that the pitch does not reach a value higher than is desired.
	if ( audio.pitch > 2.0 ) {
		audio.pitch = 2.0;
	}

	// finally, apply the values to the wheels.	The torque applied is divided by the current gear, and
	// multiplied by the user input variable.
	//FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
	//FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
	
	FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * 1;
	FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * 1;
		
	// the steer angle is an arbitrary value multiplied by the user input.
	
	direction = GameObject.Find("LeftHand").transform.position.y-GameObject.Find("RightHand").transform.position.y;
	print("leftHand: " + GameObject.Find("LeftHand").transform.position.y + " Righthand: " + GameObject.Find("RightHand").transform.position.y);
	FrontLeftWheel.steerAngle = 4.5 * direction;
	FrontRightWheel.steerAngle = 4.5 * direction;
		
	//FrontLeftWheel.steerAngle = 10 * Input.GetAxis("Horizontal");
	//FrontRightWheel.steerAngle = 10 * Input.GetAxis("Horizontal");
}


	 
function OnTriggerEnter (other : Collider) {
		Destroy(other.gameObject);
		time = time + 7;
		audio.Play();
		hits -= 1;
		}

function OnCollisionEnter(collision : Collision) {
		
		if (collision.collider.name == "Wall-Left" || collision.collider.name == "Wall-Right") {
		transform.position = new Vector3(0, transform.position.y, transform.position.z);
		transform.rotation = Quaternion.identity;
		FrontRightWheel.motorTorque = 0;
		FrontLeftWheel.motorTorque = 0;
		
		}		
	}
	
	
function ShiftGears() {
	// this funciton shifts the gears of the vehcile, it loops through all the gears, checking which will make
	// the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
	if ( EngineRPM >= MaxEngineRPM ) {
		var AppropriateGear : int = CurrentGear;
		
		for ( var i = 0; i < GearRatio.length; i ++ ) {
			if ( FrontLeftWheel.rpm * GearRatio[i] < MaxEngineRPM ) {
				AppropriateGear = i;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
	
	if ( EngineRPM <= MinEngineRPM ) {
		AppropriateGear = CurrentGear;
		
		for ( var j = GearRatio.length-1; j >= 0; j -- ) {
			if ( FrontLeftWheel.rpm * GearRatio[j] > MinEngineRPM ) {
				AppropriateGear = j;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
}