import Oidc from 'oidc-client';

var config = {
    authority: "http://localhost:6565",
    client_id: "frontentclient",
    redirect_uri: "http://localhost:8080/callback",
    response_type: "id_token token",
    scope:"openid profile pd_api email",
    post_logout_redirect_uri : "http://localhost:8080/",
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),

    automaticSilentRenew: true,
    silent_redirect_uri: 'http://localhost:8080/static/silent-renew.html',
    accessTokenExpiringNotificationTime: 10
};
var mgr = new Oidc.UserManager(config);

mgr.events.addAccessTokenExpiring(function () {
    console.log('AccessToken Expiring：', arguments);
  });
  
mgr.events.addAccessTokenExpired(function () {
    console.log('AccessToken Expired：', arguments);

    mgr.signoutRedirect().then(function (resp) {
        console.log('signed out', resp);
    }).catch(function (err) {
        console.log(err)
    });
});

export class AuthService {
 // Renew the token manually
 renewToken () {
    let self = this
    return new Promise((resolve, reject) => {
      mgr.signinSilent().then(function (user) {
        if (user == null) {
          self.signIn(null)
        } else{
          return resolve(user)
        }
      }).catch(function (err) {
        console.log(err)
        return reject(err)
      });
    })
  }

  // Get the user who is logged in
  getUser () {
    let self = this
    return new Promise((resolve, reject) => {
      mgr.getUser().then(function (user) {
        if (user == null) {
          self.signIn()
          return resolve(null)
        } else{          
          return resolve(user)
        }
      }).catch(function (err) {
        console.log(err)
        return reject(err)
      });
    })
  }

  // Check if there is any user logged in
  getSignedIn () {
    let self = this
    return new Promise((resolve, reject) => {
      mgr.getUser().then(function (user) {
        if (user == null) {
          //self.signIn()
          return resolve(false)
        } else{
          return resolve(true)
        }
      }).catch(function (err) {
        console.log(err)
        return reject(err)
      });
    })
  }

  // Redirect of the current window to the authorization endpoint.
  signIn () {
    mgr.signinRedirect().catch(function (err) {
      console.log(err)
    })
  }
  
  // Redirect of the current window to the end session endpoint
  signOut () {    
    mgr.signoutRedirect().then(function (resp) {
      console.log('signed out', resp);
    }).catch(function (err) {
      console.log(err)
    })
  }

  // Get the profile of the user logged in
  getProfile () {
    let self = this
    return new Promise((resolve, reject) => {
      mgr.getUser().then(function (user) {
        if (user == null) {
          self.signIn()
          return resolve(null)
        } else{          
          return resolve(user.profile)
        }
      }).catch(function (err) {
        console.log(err)
        return reject(err)
      });
    })
  }

  // Get the token id
  getIdToken(){
    let self = this
    return new Promise((resolve, reject) => {
      mgr.getUser().then(function (user) {
        if (user == null) {
          self.signIn()
          return resolve(null)
        } else{          
          return resolve(user.id_token)
        }
      }).catch(function (err) {
        console.log(err)
        return reject(err)
      });
    })
  }

  // Get the session state
  getSessionState(){
    let self = this
    return new Promise((resolve, reject) => {
      mgr.getUser().then(function (user) {
        if (user == null) {
          self.signIn()
          return resolve(null)
        } else{          
          return resolve(user.session_state)
        }
      }).catch(function (err) {
        console.log(err)
        return reject(err)
      });
    })
  }

  // Get the access token of the logged in user
  getAcessToken(){
    let self = this
    return new Promise((resolve, reject) => {
      mgr.getUser().then(function (user) {
        if (user == null) {
          self.signIn()
          return resolve(null)
        } else{          
          return resolve(user.access_token)
        }
      }).catch(function (err) {
        console.log(err)
        return reject(err)
      });
    })
  }

  // Takes the scopes of the logged in user
  getScopes(){
    let self = this
    return new Promise((resolve, reject) => {
      mgr.getUser().then(function (user) {
        if (user == null) {
          self.signIn()
          return resolve(null)
        } else{          
          return resolve(user.scopes)
        }
      }).catch(function (err) {
        console.log(err)
        return reject(err)
      });
    })
  }

  // Get the user roles logged in
  getRole () {
    let self = this
    return new Promise((resolve, reject) => {
      mgr.getUser().then(function (user) {
        if (user == null) {
          self.signIn()
          return resolve(null)
        } else{          
          return resolve(user.profile.role)
        }
      }).catch(function (err) {
        console.log(err)
        return reject(err)
      });
    })
  }
};

export default mgr;