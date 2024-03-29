﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	[SerializeField] GameObject toFollow;

	Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
		offset = transform.position - toFollow.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		transform.position = offset + toFollow.transform.position;
	}
}
