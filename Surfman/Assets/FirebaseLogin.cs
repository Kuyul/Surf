using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseLogin : MonoBehaviour {

    public LoginHandler AuthFirebase;

	// Authorise Firebase with facebook account
	void Awake () {
        AuthFirebase.SigninWithCredentialAsync();

    }
}
