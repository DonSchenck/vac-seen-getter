# vac-seen-getter
## What is this?
This repo is Part 1 (of eight) of a workshop/activity/tutorial that comprises the "Vac-Seen System". This system is associated with, and specifically created for, the [Red Hat OpenShift Sandbox](https://developers.redhat.com/developer-sandbox).

At the end of this tutorial you will have an instance of a small website that is running in an OpenShift cluster.

## Need help?
If you need help or get stuck, email devsandbox@redhat.com.
If you find a defect, [create an Issue](https://docs.github.com/en/issues/tracking-your-work-with-issues/creating-an-issue) in this repository.

## Prerequisites
The following **three** prerequisites are necessary:
1. An account in [OpenShift Sandbox](https://developers.redhat.com/developer-sandbox) (No problem; it's free). This is not actually *necessary*, since you can use this tutorial with any OpenShift cluster.
1. The `oc` command-line tool for OpenShift. There are instructions later in this article for the installation of `oc`.
1. Your machine will need access to a command line; Bash or PowerShell, either is fine.
1. This activity assumes you have completed Parts 1-7 of the "Vac-Seen System".

## All Operating Systems Welcome
You can use this activity regardless of whether your PC runs Windows, Linux, or macOS.

## Overview
Retrieves vaccination summary. Starts at the date supplied and goes back in time the number of days supplied.

For example: 20220901, 30 will begin at 9/1/2022 and summarize each day up to 30 days prior to that date.

Here's an example of calling this API (from a Blazor server-side app):  

string queryDate = DateTime.Now.ToString("yyyyMMdd");  
string country = "US";  
string URI = $"http://vac-seen-getter:8080/vaccination/GetVaccinationSummary/?queryDate={queryDate}&countryCode={country}&DaysToGoBack=30";  

## Creating this app in OpenShift
### Step 1
To build this microservice from source code in OpenShift, run the following command:

`oc new-app --name=vac-seen-getter dotnet:6.0~https://github.com/donschenck/vac-seen-getter -e MYSQL_CONNECTION_STRING="Server=mariadb;User ID=root;Password=admin;Database=vaxdb;"`
