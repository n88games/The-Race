using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dice : MonoBehaviour {

	private Rigidbody rg;
	private Vector3 throwDirection;
    public CamFollow camFollow;
    


	/**
	 * Initial setup of dice
	 * 
	 * blue axis is normal to 1 side
	 * red axis is normal to 3 side
	 * yellow is normal to 5 side
	 * all above is axis in local space
	 * */


	//map to store the number of each side of the dice and its normal direction in LOCAL SPACE
	private Dictionary<int,Vector3> numberAndItsNormal = new Dictionary<int, Vector3>();
    protected MeshRenderer mr;


    private Vector3 start, end;

	// Use this for initialization
	void Start () {

		rg = GetComponent<Rigidbody>();
		//The intial direction  of dice (blue axis) is the direction of thrwoing so cache it 
		throwDirection = this.transform.TransformDirection(Vector3.forward); //Transforms from local space to world space.;


		//initialize the dice number and normals
		numberAndItsNormal.Add(1,Vector3.forward);
		numberAndItsNormal.Add(6,-Vector3.forward);

		numberAndItsNormal.Add(5,Vector3.up);
		numberAndItsNormal.Add(2,-Vector3.up);

		numberAndItsNormal.Add(3,Vector3.right);
		numberAndItsNormal.Add(4,-Vector3.right);

        mr = GetComponent<MeshRenderer>();

        start = transform.position;
        end = start + throwDirection * 2;

	}


    /// <summary>
    /// register to RollDice event
    /// </summary>
    void OnEnable()
    {
        MenuEventDispatcher.OnThrowDice += this.ThrowDice;
    }


    /// <summary>
    /// unregister to roll dice event
    /// </summary>
    void OnDisable()
    {

        MenuEventDispatcher.OnThrowDice -= this.ThrowDice;
    }


	public void ThrowDice()
	{
        mr.enabled = true;
        camFollow.CanFollow =  true;
		Vector3 randomRotation = new Vector3(Random.Range(100,360),225,Random.Range(100,360));
		rg.rotation = Quaternion.Euler(randomRotation);
		rg.AddForce(Random.Range(3.2f,4.15f)*throwDirection,ForceMode.VelocityChange);
		rg.useGravity = true;
		StartCoroutine(WaitForDiceToStop());
	}


	private IEnumerator WaitForDiceToStop()
	{
        Time.timeScale = 0.7f;
		while(!rg.IsSleeping())
		{
			yield return new WaitForFixedUpdate();
		}
        Time.timeScale = 1;
		int result = this.getDiceResult();
		Debug.Log("Result = "+result);
	}



	private int getDiceResult()
	{

		//take dot product with each side direction , the on with positive is the one
		foreach(KeyValuePair<int,Vector3> pair in numberAndItsNormal)
		{
			Vector3 localSideNormal =  pair.Value;
			Vector3 worldSideNormal =  this.transform.TransformDirection(localSideNormal); //Transforms from local space to world space.
			if (Vector3.Dot(worldSideNormal,Vector3.up) > 0.5f)
			{
				return pair.Key;
			}
		}

		return 0;

	}


    public void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(start, end);

    
    }





}
