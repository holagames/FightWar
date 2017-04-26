using UnityEngine;
using System.Collections;
using System;

public class SFOnlineUser {

	private long id;
	private string channelId;
	private string channelUserId;
	
	private string userName;
	private string token;
	private string productCode;

	public SFOnlineUser(long id, string channelId, string channelUserId,
			string userName, string token, string productCode) {
		this.id = id;
		this.channelId = channelId;
		this.channelUserId = channelUserId;
		
		this.userName = userName;
		this.token = token;
		this.productCode = productCode;
	}
	
	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public string getChannelId() {
		return channelId;
	}

	public void setChannelId(string channelId) {
		this.channelId = channelId;
	}

	public string getChannelUserId() {
		return channelUserId;
	}

	public void setChannelUserId(string channelUserId) {
		this.channelUserId = channelUserId;
	}

	public string getUserName() {
		return userName;
	}

	public void setUserName(string userName) {
		this.userName = userName;
	}

	public string getToken() {
		return token;
	}

	public void setToken(string token) {
		this.token = token;
	}

	public string getProductCode() {
		return productCode;
	}

	public void setProductCode(string productCode) {
		this.productCode = productCode;
	}

}

